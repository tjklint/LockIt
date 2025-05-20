using LockIt.ViewModels;

namespace LockIt.Views

{
    /// <summary>
    /// Represents the menu interface for a visitor user, providing options such as accessing the camera or opening a lock.
    /// </summary>
    public partial class VisitorMenuPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisitorMenuPage"/> class and loads its components.
        /// </summary>
        public VisitorMenuPage(MenuPageViewModel viewmodel)
        {
            InitializeComponent();
            BindingContext = viewmodel;
        }

        /// <summary>
        /// Handles the event when the "Access Camera" button is clicked.
        /// Navigates the user to the <c>CameraPage</c>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        /// <example>
        /// <code>
        /// OnAccessCameraClicked(this, EventArgs.Empty);
        /// </code>
        /// </example>
        private async void OnAccessCameraClicked(object sender, EventArgs e)
        {
            var cameraViewModel = new CameraPageViewModel();
            var cameraPage = new CameraPage(cameraViewModel);

            await Navigation.PushAsync(cameraPage);
        }

        /// <summary>
        /// Handles the event when the "Open Lock" button is clicked.
        /// Navigates the user to the <c>OpenLockPage</c>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        /// <example>
        /// <code>
        /// OnOpenLockClicked(this, EventArgs.Empty);
        /// </code>
        /// </example>
        private async void OnOpenLockClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("OpenLockPage");
        }
    }
}
