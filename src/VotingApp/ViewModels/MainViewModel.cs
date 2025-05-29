using System.ComponentModel;
using VotingApp.Models;

namespace VotingApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public bool SurveyExists(string title)
        {
            using (var context = new AppDbContext())
            {
                return (context.Surveys.Where(s => s.Title == title).Count() > 0);
            }
        }

        public int CountVotes(string title)
        {
            using (var context = new AppDbContext())
            {
                var voteCount = context.Surveys
                .Where(s => s.Title == title)
                .SelectMany(s => s.Options)
                .SelectMany(o => o.Votes)
                .Count();

                return voteCount;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

