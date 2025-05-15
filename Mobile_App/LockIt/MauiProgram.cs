using Microsoft.Extensions.Logging;
using DotNetEnv;
using LockIt.DataRepos;
using LockIt.Services;

namespace LockIt
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var baseDir = AppContext.BaseDirectory;
            var envPath = Path.Combine(baseDir, ".env");
            DotNetEnv.Env.Load(envPath);

            var testApiKey = "AIzaSyDunVNhDF035vH9KHsNIc-LvbkF-qegyvw";

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
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
