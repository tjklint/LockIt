using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LockIt.Repos;
using LockIt.Views;

namespace LockIt.ViewModels
{
    /// <summary>
    /// ViewModel handling login logic and navigation for the login screen.
    /// </summary>
    public class LoginViewModel : ObservableObject
    {
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

        /// <summary>
        /// Gets the command used to initiate the login process.
        /// </summary>
        public IAsyncRelayCommand LoginCommand { get; }

        /// <summary>
        /// Gets the command used to navigate to the registration page.
        /// </summary>
        public IAsyncRelayCommand NavigateToRegisterCommand { get; }

        private readonly FirebaseAuthRepository _authRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class and sets up commands and authentication repository.
        /// </summary>
        public LoginViewModel()
        {
            _authRepo = new FirebaseAuthRepository();
            LoginCommand = new AsyncRelayCommand(LoginAsync);
            NavigateToRegisterCommand = new AsyncRelayCommand(NavigateToRegisterAsync);
        }

        /// <summary>
        /// Performs the login operation using the Firebase authentication repository.
        /// Navigates to the SelectUserTypePage on successful login.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <example>
        /// <code>
        /// await viewModel.LoginCommand.ExecuteAsync(null);
        /// </code>
        /// </example>
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

        /// <summary>
        /// Navigates the user to the registration page.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <example>
        /// <code>
        /// await viewModel.NavigateToRegisterCommand.ExecuteAsync(null);
        /// </code>
        /// </example>
        private async Task NavigateToRegisterAsync()
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }
    }
}
