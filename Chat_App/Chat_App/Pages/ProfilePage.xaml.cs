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

        private void LogoutButton_Clicked(object sender, EventArgs e)
        {

            App.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}