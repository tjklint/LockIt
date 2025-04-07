using ComfuritySolutions.ViewModels;

namespace ComfuritySolutions.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        BindingContext = new HomePageViewModel();
    }
}