using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LockIt.Repos;
using LockIt.Views;
using Microsoft.Maui.Controls;

namespace LockIt.ViewModels
{
    /// <summary>
    /// ViewModel for handling user registration logic and navigation.
    /// </summary>
    public class RegisterViewModel : ObservableObject
    {
        private string _username;

        /// <summary>
        /// Gets or sets the username entered by the user.
        /// </summary>
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _email;

        /// <summary>
        /// Gets or sets the email entered by the user.
        /// </summary>
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;

        /// <summary>
        /// Gets or sets the password entered by the user.
        /// </summary>
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmPassword;

        /// <summary>
        /// Gets or sets the confirmation password entered by the user.
        /// </summary>
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        /// <summary>
        /// Gets the command used to register a new user.
        /// </summary>
        public IAsyncRelayCommand RegisterCommand { get; }

        /// <summary>
        /// Gets the command used to navigate to the login page.
        /// </summary>
        public IAsyncRelayCommand NavigateToLoginCommand { get; }

        private readonly FirebaseAuthRepository _authRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterViewModel"/> class,
        /// sets up authentication repository and initializes commands.
        /// </summary>
        public RegisterViewModel()
        {
            _authRepo = new FirebaseAuthRepository();
            RegisterCommand = new AsyncRelayCommand(RegisterAsync);
            NavigateToLoginCommand = new AsyncRelayCommand(NavigateToLoginAsync);
        }

        /// <summary>
        /// Handles user registration logic including input validation,
        /// password matching, and calling the Firebase registration API.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <example>
        /// <code>
        /// await viewModel.RegisterCommand.ExecuteAsync(null);
        /// </code>
        /// </example>
        // TODO: Check email format
        private async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert("Validation Error", "All fields are required.", "OK");
                return;
            }

            if (Password != ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Validation Error", "Passwords do not match.", "OK");
                return;
            }

            var result = await _authRepo.RegisterAsync(Email, Password);
            if (result != null && !string.IsNullOrEmpty(result.idToken))
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Registration successful.", "OK");
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Registration failed. Please try again.", "OK");
            }
        }

        /// <summary>
        /// Navigates the user to the login page.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <example>
        /// <code>
        /// await viewModel.NavigateToLoginCommand.ExecuteAsync(null);
        /// </code>
        /// </example>
        private async Task NavigateToLoginAsync()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
