using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProblemReporter.Models;

namespace ProblemReporter.Data;

public class ProblemDbContext : IdentityDbContext
{
    public ProblemDbContext(DbContextOptions<ProblemDbContext> options)
        : base(options)
    {
    }

    public DbSet<Problem> Problems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Problem>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}