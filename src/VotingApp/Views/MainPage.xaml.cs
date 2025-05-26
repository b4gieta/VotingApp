using VotingApp.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace VotingApp
{
    public partial class MainPage : ContentPage
    {
        AppDbContext _dbContext;

        public MainPage(AppDbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            if (_dbContext.Database.CanConnect())
            {
                CounterBtn.Text = "działa";
            } 
            else CounterBtn.Text = "nie działa";
        }
    }
}
