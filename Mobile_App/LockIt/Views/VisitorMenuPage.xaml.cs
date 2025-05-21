// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: Controls the visitor-facing menu interface, showing allowed features based on homeowner permissions.

using LockIt.Helpers;
using LockIt.Repos;
using LockIt.Services;
using LockIt.ViewModels;

namespace LockIt.Views
{
    /// <summary>
    /// Represents the visitor menu interface and handles visibility of features based on homeowner-defined permissions.
    /// </summary>
    public partial class VisitorMenuPage : ContentPage
    {
        /// <summary>
        /// The ViewModel used for data binding sensor values and homeowner reference.
        /// </summary>
        public MenuPageViewModel ViewModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitorMenuPage"/> class and loads the access permissions.
        /// </summary>
        /// <param name="viewmodel">The view model containing environment data and homeowner reference.</param>
        public VisitorMenuPage(MenuPageViewModel viewmodel)
        {
            InitializeComponent();
            ViewModel = viewmodel;
            BindingContext = ViewModel;

            LoadPermissions();
        }

        /// <summary>
        /// Loads visitor access permissions from the Firebase database and toggles feature visibility accordingly.
        /// </summary>
        private async void LoadPermissions()
        {
            var root = AppSettingsLoader.Load();
            var dbUrl = root.GetProperty("Firebase").GetProperty("DatabaseUrl").GetString();

            var repo = new CodeRepository(dbUrl);
            var permissions = await repo.GetVisitorPermissionsAsync(AuthService.HomeownerEmail);

            CameraButton.IsVisible = permissions?.Camera ?? false;
            LockButton.IsVisible = permissions?.Lock ?? false;
            MapButton.IsVisible = permissions?.Map ?? false;
        }

        /// <summary>
        /// Handles the "Access Camera" button click event.
        /// Navigates the visitor to the camera viewing page.
        /// </summary>
        /// <param name="sender">The button clicked.</param>
        /// <param name="e">The event data.</param>
        private async void OnAccessCameraClicked(object sender, EventArgs e)
        {
            var cameraViewModel = new CameraPageViewModel();
            var cameraPage = new CameraPage(cameraViewModel);

            await Navigation.PushAsync(cameraPage);
        }

        /// <summary>
        /// Handles the "Open Lock" button click event.
        /// Navigates the visitor to the lock control page.
        /// </summary>
        /// <param name="sender">The button clicked.</param>
        /// <param name="e">The event data.</param>
        private async void OnOpenLockClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("OpenLockPage");
        }

        /// <summary>
        /// Handles the "Map" button click event.
        /// Navigates the visitor to the GPS map tracking page.
        /// </summary>
        /// <param name="sender">The button clicked.</param>
        /// <param name="e">The event data.</param>
        private async void OnMapClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(FindMyPage));
        }
    }
}
