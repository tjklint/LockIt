using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LockIt.Repos;

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
        private readonly FirebaseAuthRepository _authRepo;

        public LoginViewModel()
        {
            _authRepo = new FirebaseAuthRepository();
            LoginCommand = new AsyncRelayCommand(LoginAsync);
        }

        private async Task LoginAsync()
        {
            var result = await _authRepo.LoginAsync(Email, Password);
            if (result != null && !string.IsNullOrEmpty(result.idToken))
            {
                // TODO: Handle success
            }
            else
            {
                // TODO: Handle error 
            }
        }
    }
}

