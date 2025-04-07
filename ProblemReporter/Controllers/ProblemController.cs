using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProblemReporter.Models;
using ProblemReporter.Models.Dtos;
using ProblemReporter.Services;

namespace ProblemReporter.Controllers;

[ApiController, Route("[controller]")]
public class ProblemController : Controller
{
    private readonly ProblemService _problemService;
    private readonly UserManager<IdentityUser> _userManager;
    
    public ProblemController(ProblemService problemService, UserManager<IdentityUser> userManager)
    {
        _problemService = problemService;
        _userManager = userManager;
    }
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllProblems()
    {
        // serviceből összes problem -> id alapján manager-rel lekérni a nevet
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateProblem([FromBody] ProblemCreateDto problemCreateDto)
    {
        var user = await _userManager.GetUserAsync(User);
        var problem = new Problem()
        {
            Description = problemCreateDto.Description,
            SubmittedAt = DateTime.UtcNow,
            UserId = user.Id
        };
        await _problemService.CreateProblemAsync(problem);
        return CreatedAtAction(nameof(CreateProblem), new { id = problem.Id }, problem);
    }
}