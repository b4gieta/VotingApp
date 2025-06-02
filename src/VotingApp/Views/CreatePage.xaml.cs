using System.Diagnostics;
using VotingApp.Models;

namespace VotingApp
{
    public partial class CreatePage : ContentPage
    {
        public CreatePage()
        {
            InitializeComponent();
            OptionCountPicker.SelectedItem = 2;
            UpdateOptionFields(2);
        }

        private void OptionCountChanged(object sender, EventArgs e)
        {
            if (OptionCountPicker.SelectedItem is int count) UpdateOptionFields(count);
        }

        private void UpdateOptionFields(int count)
        {
            OptionsContainer.Children.Clear();
            for (int i = 1; i <= count; i++)
            {
                OptionsContainer.Children.Add(new Entry
                {
                    Placeholder = $"Opcja {i}",
                    Margin = new Thickness(0, 5)
                });
            }
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }

        private async void OnSendClicked(object sender, EventArgs e)
        {
            string surveyTitle = Title.Text?.Trim();

            List<string> options = new List<string>();
            foreach (var child in OptionsContainer.Children)
            {
                if (child is Entry entry)
                {
                    string optionText = entry.Text?.Trim();
                    if (!string.IsNullOrEmpty(optionText)) options.Add(optionText);
                }
            }

            DateTime expiration = ExpirationDatePicker.Date + ExpirationTimePicker.Time;

            int id = await CreateSurvey(surveyTitle, options, expiration);

            await Shell.Current.GoToAsync($"//{nameof(SurveyPage)}?id={id}");
        }

        private async Task<int> CreateSurvey(string title, List<string> options, DateTime expirationTime)
        {
            using (var context = new AppDbContext())
            {
                Random rnd = new Random();
                Survey s = new Survey { Title = title, ExpirationTime = expirationTime };
                s.Options = new List<Option>();

                foreach (string o in options)
                {
                    Option op = new Option { Name = o };
                    for (int i = 0; i < rnd.Next(1, 15); i++) op.Votes.Add(new Vote());
                    s.Options.Add(op);
                }
                await context.Surveys.AddAsync(s);
                await context.SaveChangesAsync();
                return s.Id;
            }
        }
    }
}