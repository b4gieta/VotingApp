using System.ComponentModel;
using VotingApp.Models;

namespace VotingApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public bool SurveyExists(int id)
        {
            using (var context = new AppDbContext())
            {
                return (context.Surveys.Where(s => s.Id == id).Count() > 0);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

