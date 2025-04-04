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
    }
}
