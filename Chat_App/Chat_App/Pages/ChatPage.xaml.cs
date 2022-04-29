using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        public ChatPage()
        {
            InitializeComponent();
        }

        private async void Search_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchEntry.Text))
            {
                await DisplayAlert("Error", "Missing field/s.", "Okay");
            }
            else
            {
                if (SearchEntry.Text == "test@email.com")
                {
                    App.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    await DisplayAlert("", "User not found.", "Okay");
                }
            }
        }
    }
}