using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Maps;
using LockIt.DataRepos;

namespace LockIt.ViewModels
{
    public class FindMyPageViewModel : INotifyPropertyChanged
    {
        private readonly UserDataRepo _userDataRepo;

        private Location _doorbellLocation;
        public Location DoorbellLocation
        {
            get => _doorbellLocation;
            set
            {
                _doorbellLocation = value;
                OnPropertyChanged();
            }
        }

        public FindMyPageViewModel(UserDataRepo userDataRepo)
        {
            _userDataRepo = userDataRepo;
            UpdateLocation();

            _userDataRepo.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(_userDataRepo.Motion) ||
                    args.PropertyName == nameof(_userDataRepo.Latitude) ||
                    args.PropertyName == nameof(_userDataRepo.Longitude))
                {
                    UpdateLocation();
                }
            };
        }

        private void UpdateLocation()
        {
            DoorbellLocation = new Location(_userDataRepo.Latitude, _userDataRepo.Longitude);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
