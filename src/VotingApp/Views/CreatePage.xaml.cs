using VotingApp.ViewModels;

namespace VotingApp
{
    public partial class CreatePage : ContentPage
    {
        private CreateViewModel _viewModel = new CreateViewModel();

        public CreatePage()
        {
            InitializeComponent();
            BindingContext = _viewModel;

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

        private async void OnSendClicked(object sender, EventArgs e)
        {
            string title = Title.Text?.Trim();
            if (string.IsNullOrEmpty(title)) title = "";

            var options = new List<string>();
            foreach (var child in OptionsContainer.Children)
            {
                if (child is Entry entry)
                {
                    var text = entry.Text?.Trim();
                    if (!string.IsNullOrEmpty(text)) options.Add(text);
                    else options.Add("");
                }
            }

            DateTime expiration = ExpirationDatePicker.Date + ExpirationTimePicker.Time;

            int id = await _viewModel.CreateSurveyAsync(title, options, expiration);
            await Shell.Current.GoToAsync($"//{nameof(SurveyPage)}?id={id}");
            ClearPage();
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            ClearPage();
        }

        private void ClearPage()
        {
            Title.Text = "";
            OptionCountPicker.SelectedItem = 2;
            UpdateOptionFields(2);
            ExpirationDatePicker.Date = DateTime.Now;
            ExpirationTimePicker.Time = DateTime.Now.TimeOfDay;
        }
    }
}
