using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProblemReporter.Models;

public class Problem
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
        
    public string UserId { get; set; } = string.Empty;
    [ForeignKey("UserId")]
    public virtual IdentityUser User { get; set; } 
}