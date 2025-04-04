using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LockIt.Repos;
using LockIt.Views;
using Microsoft.Maui.Controls;

namespace LockIt.ViewModels
{
    public class RegisterViewModel : ObservableObject
    {
        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public IAsyncRelayCommand RegisterCommand { get; }
        public IAsyncRelayCommand NavigateToLoginCommand { get; }

        private readonly FirebaseAuthRepository _authRepo;

        public RegisterViewModel()
        {
            _authRepo = new FirebaseAuthRepository();
            RegisterCommand = new AsyncRelayCommand(RegisterAsync);
            NavigateToLoginCommand = new AsyncRelayCommand(NavigateToLoginAsync);
        }

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

            // Validate that the password fields match.
            if (Password != ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Validation Error", "Passwords do not match.", "OK");
                return;
            }

            // TODO: Check email format

            // Call the Firebase registration service.
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

        private async Task NavigateToLoginAsync()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}

