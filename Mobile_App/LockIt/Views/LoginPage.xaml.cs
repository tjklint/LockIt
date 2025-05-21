// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: Defines the login screen behavior, binding it to the LoginViewModel and refreshing UI state.

using LockIt.ViewModels;

namespace LockIt.Views
{
    /// <summary>
    /// Represents the login screen UI logic and binds the page to the <see cref="LoginViewModel"/>.
    /// </summary>
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new LoginViewModel();
        }

        /// <summary>
        /// Overrides the OnAppearing lifecycle method to refresh the view model state when the page appears.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.RefreshState();
        }
    }
}
