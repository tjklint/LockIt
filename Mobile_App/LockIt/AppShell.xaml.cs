using LockIt.Views;

namespace LockIt
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CameraPage), typeof(CameraPage));
            Routing.RegisterRoute(nameof(OpenLockPage), typeof(OpenLockPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));
            Routing.RegisterRoute(nameof(VisitorMenuPage), typeof(VisitorMenuPage));
            Routing.RegisterRoute(nameof(SelectUserTypePage), typeof(SelectUserTypePage));
            Routing.RegisterRoute(nameof(SetLockPage), typeof(SetLockPage));
            Routing.RegisterRoute(nameof(FindMyPage), typeof(FindMyPage));
            Routing.RegisterRoute(nameof(VisitorAccessPage), typeof(VisitorAccessPage));
        }
    }
}
