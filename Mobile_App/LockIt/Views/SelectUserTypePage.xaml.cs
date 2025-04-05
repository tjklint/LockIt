using Microsoft.Maui.Controls;

namespace LockIt.Views
{
    /// <summary>
    /// Allows the user to select their role type (Homeowner or Visitor) and navigates to the corresponding menu page.
    /// </summary>
    public partial class SelectUserTypePage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectUserTypePage"/> class and loads UI components.
        /// </summary>
        public SelectUserTypePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the event when the "Homeowner" option is tapped.
        /// Highlights the Homeowner frame and navigates to the homeowner <see cref="MenuPage"/>.
        /// </summary>
        /// <param name="sender">The source of the tap event.</param>
        /// <param name="e">Event arguments.</param>
        /// <example>
        /// <code>
        /// OnHomeownerTapped(this, EventArgs.Empty);
        /// </code>
        /// </example>
        private async void OnHomeownerTapped(object sender, EventArgs e)
        {
            HomeownerFrame.BorderColor = Colors.Blue;
            VisitorFrame.BorderColor = Colors.Transparent;

            await Shell.Current.GoToAsync(nameof(MenuPage));
        }

        /// <summary>
        /// Handles the event when the "Visitor" option is tapped.
        /// Highlights the Visitor frame and navigates to the <see cref="VisitorMenuPage"/>.
        /// </summary>
        /// <param name="sender">The source of the tap event.</param>
        /// <param name="e">Event arguments.</param>
        /// <example>
        /// <code>
        /// OnVisitorTapped(this, EventArgs.Empty);
        /// </code>
        /// </example>
        private async void OnVisitorTapped(object sender, EventArgs e)
        {
            VisitorFrame.BorderColor = Colors.Blue;
            HomeownerFrame.BorderColor = Colors.Transparent;

            await Shell.Current.GoToAsync(nameof(VisitorMenuPage));
        }
    }
}
