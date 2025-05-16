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
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty] private string? email;
        [ObservableProperty] private string password;

        private readonly FirebaseAuthRepository _authRepo;

        public IAsyncRelayCommand LoginCommand { get; }
        public IAsyncRelayCommand NavigateToRegisterCommand { get; }
        public IRelayCommand SignOutCommand { get; }

        public bool ShowLoginForm => !IsLoggedIn;
        public bool ShowLoggedInInfo => IsLoggedIn;

        public LoginViewModel()
        {
            _authRepo = new FirebaseAuthRepository();
            LoginCommand = new AsyncRelayCommand(LoginAsync);
            NavigateToRegisterCommand = new AsyncRelayCommand(NavigateToRegisterAsync);
            SignOutCommand = new RelayCommand(SignOut);
        }

        public bool IsLoggedIn => !string.IsNullOrEmpty(AuthService.IdToken);
        public string LoggedInEmail => AuthService.Email;

        public void RefreshState()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(LoggedInEmail));
            OnPropertyChanged(nameof(ShowLoginForm));
            OnPropertyChanged(nameof(ShowLoggedInInfo));
        }


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

        private void SignOut()
        {
            AuthService.IdToken = string.Empty;
            AuthService.Email = string.Empty;
            Email = string.Empty;
            Password = string.Empty;

            RefreshState();
        }

        private Task ShowError(string msg) =>
            Application.Current.MainPage.DisplayAlert("Login Error", msg, "OK");


    }
}
