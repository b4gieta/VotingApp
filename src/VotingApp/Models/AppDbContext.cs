using Microsoft.EntityFrameworkCore;

namespace VotingApp.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public AppDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "VotingApp.db");

            optionsBuilder.
                UseSqlite($"Filename={dbPath}");
        }
    }
}
