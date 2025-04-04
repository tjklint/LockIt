using Microsoft.Maui.Controls;

namespace LockIt.Views
{
    public partial class SelectUserTypePage : ContentPage
    {
        public SelectUserTypePage()
        {
            InitializeComponent();
        }

        private async void OnHomeownerTapped(object sender, EventArgs e)
        {
            HomeownerFrame.BorderColor = Colors.Blue;
            VisitorFrame.BorderColor = Colors.Transparent;

            await Shell.Current.GoToAsync(nameof(MenuPage));
        }

        private async void OnVisitorTapped(object sender, EventArgs e)
        {
            VisitorFrame.BorderColor = Colors.Blue;
            HomeownerFrame.BorderColor = Colors.Transparent;

            await Shell.Current.GoToAsync(nameof(MenuPage));
        }
    }
}
