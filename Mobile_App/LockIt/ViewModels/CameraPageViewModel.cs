using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;
using LockIt.DataRepos;
using System.Text.Json;
using Microsoft.Azure.Devices;
namespace LockIt.ViewModels
{
    public class CameraPageViewModel : INotifyPropertyChanged
    {
        private string connectionString;
        private string deviceId;
        private string methodName;

        private readonly UserDataRepo _userDataRepo;

        private string _motionStatus;
        public string MotionStatus
        {
            get => _motionStatus;
            set
            {
                _motionStatus = value;
                OnPropertyChanged();
            }
        }

        private ImageSource _cameraImage;
        public ImageSource CameraImage
        {
            get => _cameraImage;
            set
            {
                _cameraImage = value;
                OnPropertyChanged();
            }
        }

        public CameraPageViewModel(UserDataRepo userDataRepo)
        {
            _userDataRepo = userDataRepo;

            UpdateMotionStatus(_userDataRepo.Motion);

            _userDataRepo.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(_userDataRepo.Motion))
                {
                    UpdateMotionStatus(_userDataRepo.Motion);
                }
            };
            LoadIoTSettings();
        }

        private void LoadIoTSettings()
        {
            var root = LockIt.Helpers.AppSettingsLoader.Load();
            var iotSection = root.GetProperty("IoTHub");

            connectionString = iotSection.GetProperty("DeviceConnectionString").GetString();
            deviceId = iotSection.GetProperty("DeviceId").GetString();
            methodName = iotSection.GetProperty("MethodName").GetString();
        }

        public async Task TakePictureAsync()
        {
            try
            {
                using var client = ServiceClient.CreateFromConnectionString(connectionString);

                var payload = new { action = "TAKE_PICTURE" };

                var cloudToDeviceMethod = new CloudToDeviceMethod(methodName)
                {
                    ResponseTimeout = TimeSpan.FromSeconds(10)
                };

                cloudToDeviceMethod.SetPayloadJson(JsonSerializer.Serialize(payload));

                var response = await client.InvokeDeviceMethodAsync(deviceId, cloudToDeviceMethod);

                if (response.Status == 200)
                {
                    await Task.Delay(2000);

                    LoadCameraImage();

                    await Application.Current.MainPage.DisplayAlert("Picture Taken", "Camera image updated.", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Failed", $"Status: {response.Status}", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void UpdateMotionStatus(uint motion)
        {
            MotionStatus = motion == 1 ? "Motion at front door" : "No motion";
        }

        public void LoadCameraImage()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "output.jpg");
            if (File.Exists(filePath))
            {
                CameraImage = ImageSource.FromStream(() => File.OpenRead(filePath));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
