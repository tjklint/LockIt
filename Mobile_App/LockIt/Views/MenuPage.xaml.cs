namespace LockIt.Views;

public partial class MenuPage : ContentPage
{
	public MenuPage()
	{
		InitializeComponent();
    }

    private async void OnSetLockedClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SetLockPage));
    }
    private async void OnManageVisitorClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ManageVisitor));
    }
}