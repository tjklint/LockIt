// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: Initializes the MAUI application and registers core services and configuration.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using LockIt.DataRepos;
using LockIt.Services;
using LockIt.ViewModels;
using LockIt.Views;
using System.IO;
using System.Text.Json;
using LockIt.Repos;
using LockIt.Helpers;

namespace LockIt
{
    /// <summary>
    /// Provides the entry point for setting up the MAUI application, including service registration and configuration loading.
    /// </summary>
    public static class MauiProgram
    {
        /// <summary>
        /// Configures and builds the MAUI application with required services, fonts, and Firebase API settings.
        /// </summary>
        /// <returns>A configured <see cref="MauiApp"/> instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the Firebase API key is missing in the configuration.</exception>
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            var root = AppSettingsLoader.Load();
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

            // Register services for DI container
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
