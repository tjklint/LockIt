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
        }
    }
}
