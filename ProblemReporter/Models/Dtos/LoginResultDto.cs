namespace ProblemReporter.Models.Dtos;

public class LoginResultDto
{
    public string Token { get; set; } = "";

    public DateTime TokenExpiration { get; set; }
}