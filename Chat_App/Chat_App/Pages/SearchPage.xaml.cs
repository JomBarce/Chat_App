using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        string loggedUser = "tester@email.com";
        bool isConnected = false;

        public SearchPage()
        {
            InitializeComponent();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            bool retryBool = await DisplayAlert("Add contact", "Would you like to add <name>?", "Yes", "No");
            if (retryBool)
            {
                if (loggedUser == "test@email.com")
                {
                    await DisplayAlert("Error", "You are not allowed to add your own self.", "Okay");
                }
                else if (isConnected == true)
                {
                    await DisplayAlert("Error", "You both already have a connection.", "Okay");
                }
                else
                {
                    await Navigation.PopAsync(true);
                }
            }
        }
    }
}