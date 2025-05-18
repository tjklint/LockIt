using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LockIt.Repos;
using LockIt.Services;
using System.Text.Json;
using System.Threading.Tasks;

namespace LockIt.ViewModels
{
    public class SetLockViewModel : ObservableObject
    {
        private readonly CodeRepository _repo;
        private string _code;
        public string Code
        {
            get => _code;
            set
            {
                SetProperty(ref _code, value);
                OnPropertyChanged(nameof(HasCode));
            }
        }

        public bool HasCode => !string.IsNullOrWhiteSpace(Code);

        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand DeleteCommand { get; }
        public IAsyncRelayCommand LoadCommand { get; }

        public SetLockViewModel()
        {
            var rootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../../"));
            var appSettingsPath = Path.Combine(rootPath, "appsettings.json");

            if (!File.Exists(appSettingsPath))
                throw new FileNotFoundException("Missing appsettings.json", appSettingsPath);

            var json = File.ReadAllText(appSettingsPath);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var dbUrl = root.GetProperty("Firebase").GetProperty("DatabaseUrl").GetString();

            _repo = new CodeRepository(dbUrl, AuthService.IdToken);

            SaveCommand = new AsyncRelayCommand(SaveAsync);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
            LoadCommand = new AsyncRelayCommand(LoadAsync);

            // Initial load
            LoadCommand.Execute(null);
        }

        private async Task LoadAsync()
        {
            var existing = await _repo.GetCodeAsync();
            Code = existing;
        }

        private async Task SaveAsync()
        {
            if (await _repo.SetCodeAsync(Code))
                await Shell.Current.DisplayAlert("Saved", "Your code has been saved.", "OK");
            else
                await Shell.Current.DisplayAlert("Error", "Failed to save code.", "OK");
        }

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
