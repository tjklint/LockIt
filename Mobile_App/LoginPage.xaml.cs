using ComfuritySolutions.Views;

namespace ComfuritySolutions
{
    public partial class LoginPage : ContentPage
    {
        int count = 0;

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }

}
