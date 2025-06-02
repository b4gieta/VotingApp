namespace VotingApp
{
    public partial class SurveyPage : ContentPage, IQueryAttributable
    {
        public SurveyPage()
        {
            InitializeComponent();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("id", out var idValue))
            {
                string id = idValue as string;
                TitleLabel.Text = $"Ankieta #{id}";
            }
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}