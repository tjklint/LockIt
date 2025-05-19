using CommunityToolkit.Mvvm.ComponentModel;
using LockIt.Helpers;
using LockIt.Repos;
using LockIt.Services;
using System.Threading.Tasks;

namespace LockIt.ViewModels
{
    public partial class ManageVisitorViewModel : ObservableObject
    {
        private readonly CodeRepository _repo;

        public ManageVisitorViewModel()
        {
            var root = AppSettingsLoader.Load();
            var dbUrl = root.GetProperty("Firebase").GetProperty("DatabaseUrl").GetString();
            _repo = new CodeRepository(dbUrl, AuthService.IdToken);

            LoadPermissions();
        }

        [ObservableProperty]
        private bool cameraEnabled;

        [ObservableProperty]
        private bool mapEnabled;

        [ObservableProperty]
        private bool lockEnabled;

        partial void OnCameraEnabledChanged(bool value) => Save();
        partial void OnMapEnabledChanged(bool value) => Save();
        partial void OnLockEnabledChanged(bool value) => Save();

        private async void LoadPermissions()
        {
            var perms = await _repo.GetVisitorPermissionsAsync(AuthService.Email);
            if (perms is null) return;

            CameraEnabled = perms.Camera;
            MapEnabled = perms.Map;
            LockEnabled = perms.Lock;
        }

        private async void Save()
        {
            await _repo.SetVisitorPermissionsAsync(AuthService.Email, new()
            {
                Camera = CameraEnabled,
                Map = MapEnabled,
                Lock = LockEnabled
            });
        }
    }

    public class VisitorPermissions
    {
        public bool Camera { get; set; }
        public bool Map { get; set; }
        public bool Lock { get; set; }
    }
}
