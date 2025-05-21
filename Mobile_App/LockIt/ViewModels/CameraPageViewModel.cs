using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;

namespace LockIt.ViewModels
{
    public class CameraPageViewModel : INotifyPropertyChanged
    {
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

        public void LoadCameraImage()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "output.jpg");
            if (File.Exists(filePath))
            {
                using var stream = File.OpenRead(filePath);
                CameraImage = ImageSource.FromStream(() => File.OpenRead(filePath));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
