using LockIt.ViewModels;

namespace LockIt.Views;

public partial class HeaderView : ContentView
{
    public HeaderView()
    {
        InitializeComponent();
        BindingContext = new HeaderViewModel();

    }
}
