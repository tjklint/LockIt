using Microsoft.Extensions.Logging;
using DotNetEnv;
using LockIt.DataRepos;
using LockIt.Services;
using LockIt.ViewModels;
using LockIt.Views;

namespace LockIt
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var baseDir = AppContext.BaseDirectory;
            var envPath = Path.Combine(baseDir, ".env");
            DotNetEnv.Env.Load(envPath);

            var testApiKey = "1";

            if (string.IsNullOrEmpty(testApiKey))
            {
                throw new Exception("Firebase API key not found. Ensure the .env file is loaded.");
            }

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Jersey15-Regular.ttf", "Jersey15Regular");
                });
            builder.Services.AddSingleton<UserDataRepo>();
            builder.Services.AddSingleton<HubService>();
            builder.Services.AddSingleton<MenuPageViewModel>();
            builder.Services.AddSingleton<VisitorMenuPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
