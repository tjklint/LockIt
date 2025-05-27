// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: Controls the main homeowner menu page, allowing navigation to lock settings and visitor management.

namespace LockIt.Views
{
    /// <summary>
    /// Represents the main homeowner menu page that provides navigation to lock and visitor management settings.
    /// </summary>
    public partial class MenuPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPage"/> class and sets up the UI components.
        /// </summary>
        public MenuPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the event when the "Set Lock" button is clicked.
        /// Navigates to the <see cref="SetLockPage"/>.
        /// </summary>
        /// <param name="sender">The button that was clicked.</param>
        /// <param name="e">The event data.</param>
        private async void OnSetLockedClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SetLockPage));
        }

        /// <summary>
        /// Handles the event when the "Manage Visitor" button is clicked.
        /// Navigates to the <see cref="ManageVisitor"/> page.
        /// </summary>
        /// <param name="sender">The button that was clicked.</param>
        /// <param name="e">The event data.</param>
        private async void OnManageVisitorClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ManageVisitor));
        }

        /// <summary>
        /// Handles the "Access Camera" button click event.
        /// Navigates the visitor to the camera viewing page.
        /// </summary>
        /// <param name="sender">The button clicked.</param>
        /// <param name="e">The event data.</param>
        private async void OnAccessCameraClicked(object sender, EventArgs e)
        {
            var cameraPage = App.Current.Handler.MauiContext.Services.GetService<CameraPage>();



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
