using Microsoft.EntityFrameworkCore;

namespace VotingApp.Models
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
        
        
        /*
        public AppDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "VotingApp.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
        */
    }
}
