using Microsoft.AspNetCore.Identity;

namespace ProblemReporter.Models;

public class Problem
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
        
    public string UserId { get; set; } = string.Empty;
    public IdentityUser User { get; set; } = null!;
}