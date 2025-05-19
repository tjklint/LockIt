using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LockIt.Helpers;
using LockIt.Repos;
using LockIt.Views;
using System.Text.Json;
using System.Threading.Tasks;

namespace LockIt.ViewModels
{
    public partial class VisitorAccessViewModel : ObservableObject
    {
        private readonly CodeRepository _repo;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string code;

        public IAsyncRelayCommand SubmitCommand { get; }

        public VisitorAccessViewModel()
        {
            var root = AppSettingsLoader.Load();
            var dbUrl = root.GetProperty("Firebase").GetProperty("DatabaseUrl").GetString();

            _repo = new CodeRepository(dbUrl);
            SubmitCommand = new AsyncRelayCommand(SubmitAsync);
        }

        private async Task SubmitAsync()
        {
            var existingCode = await _repo.GetCodeAsync(email);
            if (existingCode?.Trim('"') == code)
            {
                await Shell.Current.GoToAsync(nameof(VisitorMenuPage));
            }
            else
            {
                await Shell.Current.DisplayAlert("Access Denied", "Invalid email or code.", "OK");
            }
        }
    }
}
