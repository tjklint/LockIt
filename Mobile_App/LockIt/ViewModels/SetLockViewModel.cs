// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: ViewModel for managing lock codes, including create, read, update, and delete actions.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LockIt.Helpers;
using LockIt.Repos;
using LockIt.Services;
using System.Text.Json;
using System.Threading.Tasks;

namespace LockIt.ViewModels
{
    /// <summary>
    /// ViewModel for the homeowner's Set Lock screen. Handles CRUD operations for access codes.
    /// </summary>
    public class SetLockViewModel : ObservableObject
    {
        private readonly CodeRepository _repo;

        private string _code;

        /// <summary>
        /// Gets or sets the lock access code.
        /// </summary>
        public string Code
        {
            get => _code;
            set
            {
                SetProperty(ref _code, value);
                OnPropertyChanged(nameof(HasCode));
            }
        }

        /// <summary>
        /// Indicates whether a code has been entered.
        /// </summary>
        public bool HasCode => !string.IsNullOrWhiteSpace(Code);

        /// <summary>
        /// Command to save the current code to the database.
        /// </summary>
        public IAsyncRelayCommand SaveCommand { get; }

        /// <summary>
        /// Command to delete the existing code from the database.
        /// </summary>
        public IAsyncRelayCommand DeleteCommand { get; }

        /// <summary>
        /// Command to load the current code from the database.
        /// </summary>
        public IAsyncRelayCommand LoadCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetLockViewModel"/> class.
        /// Loads the Firebase database URL from embedded app settings.
        /// </summary>
        public SetLockViewModel()
        {
            var root = AppSettingsLoader.Load();
            var dbUrl = root.GetProperty("Firebase").GetProperty("DatabaseUrl").GetString();

            _repo = new CodeRepository(dbUrl, AuthService.IdToken);

            SaveCommand = new AsyncRelayCommand(SaveAsync);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
            LoadCommand = new AsyncRelayCommand(LoadAsync);

            // Initial load
            LoadCommand.Execute(null);
        }

        /// <summary>
        /// Loads the current lock code from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task LoadAsync()
        {
            var existing = await _repo.GetCodeAsync();
            Code = existing;
        }

        /// <summary>
        /// Saves the current lock code to the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="System.Exception">Throws if saving fails.</exception>
        private async Task SaveAsync()
        {
            if (await _repo.SetCodeAsync(Code))
                await Shell.Current.DisplayAlert("Saved", "Your code has been saved.", "OK");
            else
                await Shell.Current.DisplayAlert("Error", "Failed to save code.", "OK");
        }

        /// <summary>
        /// Deletes the existing lock code from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="System.Exception">Throws if deletion fails.</exception>
        private async Task DeleteAsync()
        {
            if (await _repo.DeleteCodeAsync())
            {
                Code = null;
                await Shell.Current.DisplayAlert("Deleted", "Your code has been removed.", "OK");
            }
            else
                await Shell.Current.DisplayAlert("Error", "Failed to delete code.", "OK");
        }
    }
}
