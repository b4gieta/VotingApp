using VotingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microcharts;
using SkiaSharp;

namespace VotingApp
{
    public partial class SurveyPage : ContentPage, IQueryAttributable
    {
        private int _id;

        public SurveyPage()
        {
            InitializeComponent();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("id", out var idValue))
            {
                string id = idValue as string;
                _id = int.Parse(id);
                Display(_id);
            }
        }

        public void Display(int id)
        {
            using (var context = new AppDbContext())
            {
                var entries = new List<ChartEntry>();

                Survey survey = context.Surveys
                    .Where(s => s.Id == id)
                    .Include(s => s.Options)
                    .ThenInclude(o => o.Votes)
                    .First();

                TitleLabel.Text = $"Ankieta #{id}: {survey.Title}";

                ExpirationLabel.Text = $"Wygasa {survey.ExpirationTime.ToString()}";

                int index = 0;
                foreach (var option in survey.Options)
                {
                    int voteCount = option.Votes.Count;

                    entries.Add(new ChartEntry(voteCount)
                    {
                        Label = option.Name,
                        ValueLabel = voteCount.ToString(),
                        Color = SKColor.Parse(PieColors[index])
                    });
                    index++;
                }

                ChartView.Chart = new PieChart
                {
                    Entries = entries,
                    LabelTextSize = 20,
                    BackgroundColor = SKColors.White
                };

                VotingButtonsStack.Children.Clear();
                foreach (var option in survey.Options)
                {
                    var button = new Button
                    {
                        Text = option.Name,
                        Command = new Command(() => VoteForOption(option.Id)),
                        MaximumWidthRequest = 500,
                        HorizontalOptions = LayoutOptions.Fill
                    };
                    VotingButtonsStack.Children.Add(button);
                }

                if (survey.ExpirationTime < DateTime.Now)
                {
                    foreach (var child in VotingButtonsStack.Children)
                    {
                        if (child is Button button) button.IsEnabled = false;
                    }
                }
            }
        }

        private async void VoteForOption(int optionId)
        {
            using (var context = new AppDbContext())
            {
                Survey survey = context.Surveys
                    .Include(s => s.Options)
                    .ThenInclude(o => o.Votes)
                    .First(s => s.Id == _id);

                var selectedOption = survey.Options.First(o => o.Id == optionId);
                selectedOption.Votes.Add(new Vote());
                await context.SaveChangesAsync();

                Display(_id);

                foreach (var child in VotingButtonsStack.Children)
                {
                    if (child is Button button) button.IsEnabled = false;
                }
            }
        }

        private string[] PieColors = { "#F44336", "#2196F3", "#4CAF50", "#FFC107" };

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}