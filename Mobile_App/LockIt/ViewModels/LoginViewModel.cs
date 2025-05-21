// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: Handles user login logic, including form validation, authentication with Firebase, and UI state updates.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LockIt.Repos;
using LockIt.Services;
using LockIt.Views;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.NetworkInformation;

namespace LockIt.ViewModels
{
    /// <summary>
    /// ViewModel responsible for managing user authentication and related UI behavior.
    /// </summary>
    public partial class LoginViewModel : ObservableObject
    {
        /// <summary>
        /// The user's email address used for login.
        /// </summary>
        [ObservableProperty] private string? email;

        /// <summary>
        /// The user's password used for login.
        /// </summary>
        [ObservableProperty] private string password;

        private readonly FirebaseAuthRepository _authRepo;

        /// <summary>
        /// Command to trigger the login process.
        /// </summary>
        public IAsyncRelayCommand LoginCommand { get; }

        /// <summary>
        /// Command to navigate to the registration page.
        /// </summary>
        public IAsyncRelayCommand NavigateToRegisterCommand { get; }

        /// <summary>
        /// Command to sign out the current user.
        /// </summary>
        public IRelayCommand SignOutCommand { get; }

        /// <summary>
        /// Indicates whether the login form should be shown.
        /// </summary>
        public bool ShowLoginForm => !IsLoggedIn;

        /// <summary>
        /// Indicates whether the signed-in user info should be shown.
        /// </summary>
        public bool ShowLoggedInInfo => IsLoggedIn;

        /// <summary>
        /// Initializes the login view model and sets up commands.
        /// </summary>
        public LoginViewModel()
        {
            _authRepo = new FirebaseAuthRepository();
            LoginCommand = new AsyncRelayCommand(LoginAsync);
            NavigateToRegisterCommand = new AsyncRelayCommand(NavigateToRegisterAsync);
            SignOutCommand = new RelayCommand(SignOut);
        }

        /// <summary>
        /// Returns whether the user is currently logged in based on presence of a token.
        /// </summary>
        public bool IsLoggedIn => !string.IsNullOrEmpty(AuthService.IdToken);

        /// <summary>
        /// Returns the logged-in user's email.
        /// </summary>
        public string LoggedInEmail => AuthService.Email;

        /// <summary>
        /// Notifies the UI to update bound properties related to authentication state.
        /// </summary>
        public void RefreshState()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(LoggedInEmail));
            OnPropertyChanged(nameof(ShowLoginForm));
            OnPropertyChanged(nameof(ShowLoggedInInfo));
        }

        /// <summary>
        /// Handles user login, including network checks and credential validation.
        /// </summary>
        /// <returns>A task representing the async login operation.</returns>
        /// <exception cref="HttpRequestException">Thrown if network is unavailable.</exception>
        /// <exception cref="Exception">Thrown if Firebase returns an unexpected error.</exception>
        private async Task LoginAsync()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                await ShowError("No internet connection.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await ShowError("Please fill in both email and password.");
                return;
            }

            try
            {
                var result = await _authRepo.LoginAsync(Email, Password);

                if (!string.IsNullOrEmpty(result?.idToken))
                {
                    AuthService.IdToken = result.idToken;
                    AuthService.Email = Email;
                    RefreshState();

                    await Shell.Current.GoToAsync(nameof(SelectUserTypePage));
                }
                else
                {
                    await ShowError("Incorrect email or password.");
                }
            }
            catch (HttpRequestException)
            {
                await ShowError("You're offline. Check your internet.");
            }
            catch (Exception ex)
            {
                await ShowError($"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Navigates to the registration page.
        /// </summary>
        /// <returns>A task representing the async navigation operation.</returns>
        /// <exception cref="Exception">Thrown if navigation fails.</exception>
        private async Task NavigateToRegisterAsync()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(RegisterPage));
            }
            catch (Exception ex)
            {
                await ShowError($"Navigation failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Logs the user out, clearing session data and refreshing state.
        /// </summary>
        private void SignOut()
        {
            AuthService.IdToken = string.Empty;
            AuthService.Email = string.Empty;
            Email = string.Empty;
            Password = string.Empty;

            RefreshState();
        }

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="msg">The message to show.</param>
        /// <returns>A task representing the alert display.</returns>
        private Task ShowError(string msg) =>
            Application.Current.MainPage.DisplayAlert("Login Error", msg, "OK");
    }
}
