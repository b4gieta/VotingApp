using VotingApp.Models;
using System.Diagnostics;

namespace VotingApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                if (context.Database.CanConnect()) CounterBtn.Text = context.Surveys.Count().ToString();
                else CounterBtn.Text = "nie działa";

                context.Surveys.Add(new Survey { Title = "aaa" });
                context.SaveChanges();
                Debug.WriteLine(context.Surveys.Count().ToString());
            }
        }
    }
}
