using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProblemReporter.Models.Dtos;

namespace ProblemReporter.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task Register(UserCreateDto dto)
    {
        var user = new IdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            EmailConfirmed = true
        };
        await _userManager.CreateAsync(user, dto.Password);

        if (_userManager.Users.Count() == 1)
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _userManager.AddToRoleAsync(user, "Admin");
        }
        else
        {
            await _userManager.AddToRoleAsync(user, "Worker");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null) return BadRequest("Nincs ilyen user");
        var result = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!result) return BadRequest("Nem jó a jelszó");
        var claim = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.NameIdentifier, user.Id)
        };
        claim.AddRange(
            from role in await _userManager.GetRolesAsync(user) 
            select new Claim(ClaimTypes.Role, role));
        
        const int expiryInMinutes = 24 * 60;
        var token = GenerateAccessToken(claim, expiryInMinutes);

        return Ok(new LoginResultDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            TokenExpiration = DateTime.Now.AddMinutes(expiryInMinutes)
        });
    }
    private JwtSecurityToken GenerateAccessToken(IEnumerable<Claim>? claims, int expiryInMinutes)
    {
        var signinKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["jwt:key"] ?? throw new Exception("jwt:key not found in appsettings.json")));

        return new JwtSecurityToken(
            issuer: "problemreporter.com", //egyezzen a program.cs-ben beállított issuer-rel !!
            audience: "problemreporter.com", //same
            claims: claims?.ToArray(),
            expires: DateTime.Now.AddMinutes(expiryInMinutes),
            signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
        );
    }
}