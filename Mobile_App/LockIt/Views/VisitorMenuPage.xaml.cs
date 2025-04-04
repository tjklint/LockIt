namespace LockIt.Views;

public partial class VisitorMenuPage : ContentPage
{
	public VisitorMenuPage()
	{
		InitializeComponent();
	}

    private async void OnAccessCameraClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("CameraPage");
    }

    private async void OnOpenLockClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("OpenLockPage"); 
    }
}