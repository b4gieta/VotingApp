using Microsoft.EntityFrameworkCore;

namespace VotingApp.Models
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items) collection.Add(item);
        }
    }

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

        private static async Task AddStartingSurvey(AppDbContext context, string title, List<string> options)
        {
            Random rnd = new Random();
            Survey s = new Survey { Title = title, ExpirationTime = DateTime.Now.AddHours(1) };
            s.Options = new List<Option>();

            foreach (string o in options)
            {
                Option op = new Option { Name = o};
                for (int i = 0; i < rnd.Next(1, 15); i++) op.Votes.Add(new Vote());
                s.Options.Add(op);
            }
            await context.Surveys.AddAsync(s);
        }

        public static async Task SetStartingData()
        {
            using (var context = new AppDbContext())
            {
                foreach (var entity in context.Surveys) context.Surveys.Remove(entity);
                foreach (var entity in context.Options) context.Options.Remove(entity);
                foreach (var entity in context.Votes) context.Votes.Remove(entity);

                List<string> options1 = new List<string>
                {
                    "a1", "a2", "a3", "a4"
                };
                await AddStartingSurvey(context, "aaa", options1);

                List<string> options2 = new List<string>
                {
                    "hm", "hm", "hm"
                };
                await AddStartingSurvey(context, "hmm", options2);

                List<string> options3 = new List<string>
                {
                    "test1", "test2", "test3", "test4"
                };
                await AddStartingSurvey(context, "test", options3);

                await context.SaveChangesAsync();
            }
        }
    }
}
