using VotingApp.Models;

namespace VotingApp.ViewModels
{
    public class CreateViewModel
    {
        public string Title { get; set; }
        public DateTime ExpirationDate { get; set; } = DateTime.Today;
        public TimeSpan ExpirationTime { get; set; } = TimeSpan.FromHours(12);

        public async Task<int> CreateSurveyAsync(string title, List<string> options, DateTime expiration)
        {
            using ( var context = new AppDbContext())
            {
                Random rnd = new Random();
                Survey survey = new Survey { Title = title, ExpirationTime = expiration };

                survey.Options = new List<Option>();

                foreach (string o in options)
                {
                    Option op = new Option { Name = o };
                    for (int i = 0; i < rnd.Next(1, 15); i++) op.Votes.Add(new Vote());
                    survey.Options.Add(op);
                }

                await context.Surveys.AddAsync(survey);
                await context.SaveChangesAsync();

                return survey.Id;
            }
        }
    }
}
