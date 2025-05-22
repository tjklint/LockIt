using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;
using LockIt.DataRepos;

namespace LockIt.ViewModels
{
    public class CameraPageViewModel : INotifyPropertyChanged
    {
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
