using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using LockIt.DataRepos;
using LockIt.Services;
using LockIt.ViewModels;
using LockIt.Views;
using System.IO;
using System.Text.Json;
using LockIt.Repos;

namespace LockIt
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            var appSettingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

            if (!File.Exists(appSettingsPath))
                throw new FileNotFoundException("Missing appsettings.json", appSettingsPath);

            var json = File.ReadAllText(appSettingsPath);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var firebaseApiKey = root.GetProperty("Firebase").GetProperty("ApiKey").GetString();

            if (string.IsNullOrEmpty(firebaseApiKey))
                throw new InvalidOperationException("FIREBASE_API_KEY is not set in appsettings.json.");

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Jersey15-Regular.ttf", "Jersey15Regular");
                });

            // Register services
            builder.Services.AddSingleton<UserDataRepo>();
            builder.Services.AddSingleton<HubService>();
            builder.Services.AddSingleton<MenuPageViewModel>();
            builder.Services.AddSingleton<VisitorMenuPage>();
            builder.Services.AddSingleton<FirebaseAuthRepository>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}