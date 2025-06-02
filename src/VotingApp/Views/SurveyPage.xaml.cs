using VotingApp.ViewModels;

namespace VotingApp
{
    public partial class SurveyPage : ContentPage, IQueryAttributable
    {
        private SurveyViewModel _viewModel = new SurveyViewModel();

        public SurveyPage()
        {
            InitializeComponent();
            BindingContext = _viewModel;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("id", out var idValue))
            {
                string id = idValue as string;
                _viewModel.LoadSurvey(int.Parse(id));

                TitleLabel.Text = _viewModel.Title;
                ExpirationLabel.Text = _viewModel.Expiration;
                ChartView.Chart = _viewModel.Chart;

                BuildVotingButtons();
            }
        }

        private void BuildVotingButtons()
        {
            VotingButtonsStack.Children.Clear();

            foreach (var option in _viewModel.Options)
            {
                var button = new Button
                {
                    Text = option.Name,
                    HorizontalOptions = LayoutOptions.Fill,
                    MaximumWidthRequest = 500
                };

                button.Clicked += async (s, e) =>
                {
                    await _viewModel.VoteAsync(option.Id);
                    ChartView.Chart = _viewModel.Chart;
                    DisableAllButtons();
                };

                VotingButtonsStack.Children.Add(button);
            }

            if (_viewModel.IsExpired) DisableAllButtons();
        }

        private void DisableAllButtons()
        {
            foreach (var child in VotingButtonsStack.Children)
            {
                if (child is Button btn) btn.IsEnabled = false;
            }
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}
