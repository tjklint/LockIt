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

namespace LockIt.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
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

        public IAsyncRelayCommand LoginCommand { get; }
        public IAsyncRelayCommand NavigateToRegisterCommand { get; }

        private readonly FirebaseAuthRepository _authRepo;

        public LoginViewModel()
        {
            _authRepo = new FirebaseAuthRepository();
            LoginCommand = new AsyncRelayCommand(LoginAsync);
            NavigateToRegisterCommand = new AsyncRelayCommand(NavigateToRegisterAsync);
        }

        private async Task LoginAsync()
        {
            var result = await _authRepo.LoginAsync(Email, Password);
            if (result != null && !string.IsNullOrEmpty(result.idToken))
            {
                await Shell.Current.GoToAsync(nameof(SelectUserTypePage));
            }
            else
            {
                // TODO: Handle error 
            }
        }

        private async Task NavigateToRegisterAsync()
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }
    }
}

