using System.ComponentModel.DataAnnotations;

namespace ProblemReporter.Models.Dtos;

public class ProblemCreateDto
{
    [Required]
    public string Description { get; set; } = string.Empty;
}