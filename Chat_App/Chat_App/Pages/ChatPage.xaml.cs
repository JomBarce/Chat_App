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
                if (SearchEntry.Text == "tester@email.com")
                {
                    SearchEntry.Text = string.Empty;
                    await Navigation.PushAsync(new SearchPage(), true);
                }
                else
                {
                    await DisplayAlert("", "User not found.", "Okay");
                }
            }
        }
    }
}