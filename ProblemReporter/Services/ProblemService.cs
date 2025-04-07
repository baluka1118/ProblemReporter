using ProblemReporter.Data;
using ProblemReporter.Models;
using ProblemReporter.Models.Dtos;

namespace ProblemReporter.Services;

public class ProblemService
{
    private readonly ProblemDbContext _context;

    public ProblemService(ProblemDbContext context)
    {
        _context = context;
    }
    public IEnumerable<Problem> GetAllProblemsAsync()
    {

        return _context.Problems;
    }
    public async Task CreateProblemAsync(Problem problem)
    {
        await _context.Problems.AddAsync(problem); 
        await _context.SaveChangesAsync();
    }
}