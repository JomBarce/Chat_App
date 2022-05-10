using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPasswordPage : ContentPage
    {
        public ResetPasswordPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);
        }

        private async void SendEmail_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EmailEntry.Text))
            {
               EmailFrame.BorderColor = Color.Red;
               await DisplayAlert("Error", "Missing field.", "Okay");
            }
            else
            {  
                FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                res = await DependencyService.Get<iFirebaseAuth>().ResetPassword(EmailEntry.Text);

                if (res.Status == true)
                {
                    await DisplayAlert("Success", res.Response, "Okay");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", res.Response, "Okay");
                }
            }
        }

        void TextBox_Focused(object sender, FocusEventArgs e)
        {
            var textBox = sender;
            if (textBox.Equals(EmailEntry))
            {
                EmailFrame.BorderColor = Color.Black;
            }
        }
    }
}