using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using LockIt.ViewModels;
using LockIt.DataRepos;

namespace LockIt.Views
{
    public partial class FindMyPage : ContentPage
    {
        private readonly FindMyPageViewModel _viewModel;
        private readonly Pin _pin;

        public FindMyPage()
        {
            InitializeComponent();

            var userDataRepo = new UserDataRepo(); 
            _viewModel = new FindMyPageViewModel(userDataRepo);
            BindingContext = _viewModel;

            _pin = new Pin
            {
                Label = "Doorbell",
                Address = "Front Door",
                Type = PinType.Place
            };

            MapView.Pins.Add(_pin);
            UpdateMapLocation();

            _viewModel.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(_viewModel.DoorbellLocation))
                {
                    UpdateMapLocation();
                }
            };
        }

        private void UpdateMapLocation()
        {
            var location = _viewModel.DoorbellLocation;
            _pin.Location = location;
            MapView.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromMeters(300)));
        }
    }
}
