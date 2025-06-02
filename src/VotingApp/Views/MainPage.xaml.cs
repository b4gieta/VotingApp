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

        private async void OnCheckClicked(object sender, EventArgs e)
        {
            if (int.TryParse(Check.Text, out int id) && _viewModel.SurveyExists(id)) await Shell.Current.GoToAsync($"//{nameof(SurveyPage)}?id={id}");
            else CheckResult.Text = "Ankieta nie istnieje";
        }

        private async void OnResetClicked(object sender, EventArgs e)
        {
            await AppDbContext.SetStartingData();
        }

        private async void OnCreateNewClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(CreatePage)}");
        }
    }
}
