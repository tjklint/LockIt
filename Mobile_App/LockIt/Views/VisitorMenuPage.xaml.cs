using LockIt.Helpers;
using LockIt.Repos;
using LockIt.ViewModels;

namespace LockIt.Views
{
    public partial class VisitorMenuPage : ContentPage
    {
        public MenuPageViewModel ViewModel { get; set; }

        public VisitorMenuPage(MenuPageViewModel viewmodel)
        {
            InitializeComponent();
            ViewModel = viewmodel;
            BindingContext = ViewModel;

            LoadPermissions();
        }

        private async void LoadPermissions()
        {
            var root = AppSettingsLoader.Load();
            var dbUrl = root.GetProperty("Firebase").GetProperty("DatabaseUrl").GetString();

            var repo = new CodeRepository(dbUrl);
            var permissions = await repo.GetVisitorPermissionsAsync(ViewModel.HomeownerEmail);

            CameraButton.IsVisible = permissions?.Camera ?? false;
            LockButton.IsVisible = permissions?.Lock ?? false;
            MapButton.IsVisible = permissions?.Map ?? false;
        }

        private async void OnAccessCameraClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("CameraPage");
        }

        private async void OnOpenLockClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("OpenLockPage");
        }
        
        private async void OnMapClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(FindMyPage));
        }
    }
}
