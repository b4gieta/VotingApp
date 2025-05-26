using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using VotingApp.Models;

namespace VotingApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite("Data Source=VotingApp.db");
            });

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddSingleton<App>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
