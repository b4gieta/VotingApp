using Microsoft.EntityFrameworkCore;
using VotingApp.Data.Models;

namespace VotingApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
