using VotingApp.Models;
using Microcharts;
using SkiaSharp;
using Microsoft.EntityFrameworkCore;

namespace VotingApp.ViewModels
{
    public class SurveyViewModel
    {
        private int _surveyId;
        private Survey _survey;
        private string[] _pieColors = { "#F44336", "#2196F3", "#4CAF50", "#FFC107" };

        public string Title { get; private set; }
        public string Expiration { get; private set; }
        public Chart Chart { get; private set; }
        public bool IsExpired => _survey.ExpirationTime < DateTime.Now;
        public List<(int Id, string Name)> Options { get; private set; } = new();

        public void LoadSurvey(int id)
        {
            _surveyId = id;

            using var context = new AppDbContext();
            _survey = context.Surveys
                .Include(s => s.Options)
                .ThenInclude(o => o.Votes)
                .First(s => s.Id == id);

            Title = $"Ankieta #{id}: {_survey.Title}";
            Expiration = $"Wygasa {_survey.ExpirationTime:G}";
            Options = _survey.Options.Select(o => (o.Id, o.Name)).ToList();

            UpdateChart();
        }

        public async Task VoteAsync(int optionId)
        {
            using var context = new AppDbContext();
            var survey = context.Surveys
                .Include(s => s.Options)
                .ThenInclude(o => o.Votes)
                .First(s => s.Id == _surveyId);

            var option = survey.Options.First(o => o.Id == optionId);
            option.Votes.Add(new Vote());

            await context.SaveChangesAsync();
            LoadSurvey(_surveyId);
        }

        private void UpdateChart()
        {
            var entries = new List<ChartEntry>();
            int index = 0;
            foreach (var option in _survey.Options)
            {
                int voteCount = option.Votes.Count;
                entries.Add(new ChartEntry(voteCount)
                {
                    Label = option.Name,
                    ValueLabel = voteCount.ToString(),
                    Color = SKColor.Parse(_pieColors[index])
                });
                index++;
            }

            Chart = new PieChart
            {
                Entries = entries,
                LabelTextSize = 20,
                BackgroundColor = SKColors.White
            };
        }
    }
}
