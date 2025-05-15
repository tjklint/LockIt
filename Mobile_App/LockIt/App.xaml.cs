using LockIt.Services;
using LockIt.Views;

namespace LockIt
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Application.Current.Dispatcher.Dispatch(async () =>
            {
                await Shell.Current.GoToAsync(nameof(RegisterPage));
            });

        }
        protected override void OnStart()
        {
            base.OnStart();

            Task.Run(async () => await HubService.ProcessData());
        }
    }
}
