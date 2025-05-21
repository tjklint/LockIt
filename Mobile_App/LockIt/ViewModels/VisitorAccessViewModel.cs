// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: ViewModel for allowing visitors to access homeowner data using a shared access code.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LockIt.Helpers;
using LockIt.Repos;
using LockIt.Services;
using LockIt.Views;
using System.Text.Json;
using System.Threading.Tasks;

namespace LockIt.ViewModels
{
    /// <summary>
    /// ViewModel for handling visitor access to the system via homeowner email and code verification.
    /// </summary>
    public partial class VisitorAccessViewModel : ObservableObject
    {
        private readonly CodeRepository _repo;

        /// <summary>
        /// The email address of the homeowner the visitor is trying to access.
        /// </summary>
        [ObservableProperty]
        private string email;

        /// <summary>
        /// The code entered by the visitor to gain access.
        /// </summary>
        [ObservableProperty]
        private string code;

        /// <summary>
        /// Command executed when the visitor submits their email and code.
        /// </summary>
        public IAsyncRelayCommand SubmitCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitorAccessViewModel"/> class.
        /// Loads Firebase database settings from embedded configuration.
        /// </summary>
        public VisitorAccessViewModel()
        {
            var root = AppSettingsLoader.Load();
            var dbUrl = root.GetProperty("Firebase").GetProperty("DatabaseUrl").GetString();

            _repo = new CodeRepository(dbUrl);
            SubmitCommand = new AsyncRelayCommand(SubmitAsync);
        }

        /// <summary>
        /// Verifies the email and code combination and navigates the visitor to the menu page if successful.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task SubmitAsync()
        {
            var existingCode = await _repo.GetCodeAsync(email);
            if (existingCode?.Trim('"') == code)
            {
                var menuVm = new MenuPageViewModel { HomeownerEmail = Email };
                AuthService.HomeownerEmail = Email;
                await Shell.Current.Navigation.PushAsync(new VisitorMenuPage(menuVm));
            }
            else
            {
                await Shell.Current.DisplayAlert("Access Denied", "Invalid email or code.", "OK");
            }
        }
    }
}
