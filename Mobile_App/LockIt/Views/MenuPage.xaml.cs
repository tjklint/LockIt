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
    }
}
