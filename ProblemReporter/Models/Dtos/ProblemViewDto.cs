namespace ProblemReporter.Models.Dtos;

public class ProblemViewDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
    public string SubmitterName { get; set; } = string.Empty;
}