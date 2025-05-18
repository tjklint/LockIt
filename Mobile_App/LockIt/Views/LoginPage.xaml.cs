using LockIt.ViewModels;

namespace LockIt.Views;

public partial class LoginPage : ContentPage
{
    private LoginViewModel viewModel;

    public LoginPage()
    {
        InitializeComponent();
        BindingContext = viewModel = new LoginViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.RefreshState();
    }
}
