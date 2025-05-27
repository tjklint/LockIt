using LockIt.ViewModels;

namespace LockIt.Views
{
    public partial class CameraPage : ContentPage
    {

        public CameraPage(CameraPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.LoadCameraImage();
        }
        private async void OnTakePictureClicked(object sender, EventArgs e)
        {
            if (BindingContext is CameraPageViewModel vm)
            {
                await vm.TakePictureAsync();
            }
        }

    }
}
