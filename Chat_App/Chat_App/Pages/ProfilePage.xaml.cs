using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {

        public ProfilePage()
        {
            InitializeComponent();
        }

        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            bool exitBool = await DisplayAlert("Sign out", "Sign out from device?", "Yes", "No");
            if (exitBool)
            {
                await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "0");
                App.Current.MainPage = new NavigationPage(new MainPage());
            }
            
        }
    }
}