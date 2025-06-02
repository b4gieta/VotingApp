using VotingApp.Models;

namespace VotingApp.ViewModels
{
    public class MainViewModel
    {
        public bool SurveyExists(int id)
        {
            using (var context = new AppDbContext())
            {
                return (context.Surveys.Where(s => s.Id == id).Count() > 0);
            }
        }
    }
}

