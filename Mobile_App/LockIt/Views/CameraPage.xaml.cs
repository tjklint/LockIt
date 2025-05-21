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
    }
}
