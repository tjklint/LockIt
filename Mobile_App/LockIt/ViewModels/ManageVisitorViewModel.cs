// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: ViewModel to manage which features a homeowner enables or disables for visitor access.

using CommunityToolkit.Mvvm.ComponentModel;
using LockIt.Helpers;
using LockIt.Repos;
using LockIt.Services;
using System.Threading.Tasks;

namespace LockIt.ViewModels
{
    /// <summary>
    /// ViewModel for toggling visitor access permissions and syncing them with Firebase.
    /// </summary>
    public partial class ManageVisitorViewModel : ObservableObject
    {
        private readonly CodeRepository _repo;

        /// <summary>
        /// Initializes the view model and loads saved visitor permissions from the database.
        /// </summary>
        public ManageVisitorViewModel()
        {
            var root = AppSettingsLoader.Load();
            var dbUrl = root.GetProperty("Firebase").GetProperty("DatabaseUrl").GetString();
            _repo = new CodeRepository(dbUrl, AuthService.IdToken);

            LoadPermissions();
        }

        /// <summary>
        /// Gets or sets whether the visitor has access to the camera.
        /// </summary>
        [ObservableProperty]
        private bool cameraEnabled;

        /// <summary>
        /// Gets or sets whether the visitor has access to the map (FindMyPage).
        /// </summary>
        [ObservableProperty]
        private bool mapEnabled;

        /// <summary>
        /// Gets or sets whether the visitor has access to the lock functionality.
        /// </summary>
        [ObservableProperty]
        private bool lockEnabled;

        /// <summary>
        /// Called when <see cref="CameraEnabled"/> changes. Saves updated permissions to the database.
        /// </summary>
        /// <param name="value">New state of camera permission.</param>
        partial void OnCameraEnabledChanged(bool value) => Save();

        /// <summary>
        /// Called when <see cref="MapEnabled"/> changes. Saves updated permissions to the database.
        /// </summary>
        /// <param name="value">New state of map permission.</param>
        partial void OnMapEnabledChanged(bool value) => Save();

        /// <summary>
        /// Called when <see cref="LockEnabled"/> changes. Saves updated permissions to the database.
        /// </summary>
        /// <param name="value">New state of lock permission.</param>
        partial void OnLockEnabledChanged(bool value) => Save();

        /// <summary>
        /// Loads the visitor permissions for the currently authenticated user from the database.
        /// </summary>
        private async void LoadPermissions()
        {
            var perms = await _repo.GetVisitorPermissionsAsync(AuthService.Email);
            if (perms is null) return;

            CameraEnabled = perms.Camera;
            MapEnabled = perms.Map;
            LockEnabled = perms.Lock;
        }

        /// <summary>
        /// Persists the current visitor permission states to the database for the logged-in user.
        /// </summary>
        private async void Save()
        {
            await _repo.SetVisitorPermissionsAsync(AuthService.Email, new VisitorPermissions
            {
                Camera = CameraEnabled,
                Map = MapEnabled,
                Lock = LockEnabled
            });
        }
    }

    /// <summary>
    /// Data model representing visitor access permissions for specific features.
    /// </summary>
    public class VisitorPermissions
    {
        /// <summary>
        /// Gets or sets whether the visitor can use the camera.
        /// </summary>
        public bool Camera { get; set; }

        /// <summary>
        /// Gets or sets whether the visitor can view the device location on a map.
        /// </summary>
        public bool Map { get; set; }

        /// <summary>
        /// Gets or sets whether the visitor can use the lock control feature.
        /// </summary>
        public bool Lock { get; set; }
    }
}
