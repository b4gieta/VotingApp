using VotingApp.Models;
using VotingApp.ViewModels;

namespace VotingApp
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
        }

        private void OnCheckClicked(object sender, EventArgs e)
        {
            if (_viewModel.SurveyExists(Check.Text)) CheckResult.Text = $"Ankieta \"{Check.Text}\" istnieje i ma {_viewModel.CountVotes(Check.Text)} głosów";
            else CheckResult.Text = $"Ankieta \"{Check.Text}\" nie istnieje";
        }

        private async void OnResetClicked(object sender, EventArgs e)
        {
            await AppDbContext.SetStartingData();
        }
    }
}
