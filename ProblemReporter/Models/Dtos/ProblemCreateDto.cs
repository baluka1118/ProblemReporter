using System.ComponentModel.DataAnnotations;

namespace ProblemReporter.Models.Dtos;

public class ProblemCreateDto
{
    [Required]
    [MinLength(5)]
    public string Description { get; set; } = string.Empty;
}