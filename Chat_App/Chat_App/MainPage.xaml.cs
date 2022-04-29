using System;
using Xamarin.Forms;

namespace Chat_App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void SignIn_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text))
            {
                EmailFrame.BorderColor = (string.IsNullOrEmpty(EmailEntry.Text)) ? Color.Red : Color.Black;
                PasswordFrame.BorderColor = (string.IsNullOrEmpty(PasswordEntry.Text)) ? Color.Red : Color.Black;
                await DisplayAlert("Error", "Missing field/s.", "Okay");
            }
            else
            {
                if (EmailEntry.Text == "test@email.com" && PasswordEntry.Text == "12345")
                {
                    Application.Current.MainPage = new TabPage();
                }
                else
                {
                    await DisplayAlert("Error", "Account non-existent.", "Okay");
                }
            }
        }
            
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            EmailEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
        }

        private async void SignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SignupPage(), true);
        }

        private async void ForgotPassword_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResetPasswordPage(), true);
        }

        void TextBox_Focused(object sender, FocusEventArgs e)
        {
            var textBox = sender;
            if (textBox.Equals(EmailEntry))
            {
                EmailFrame.BorderColor = Color.Black;
            } else if (textBox.Equals(PasswordEntry))
            {
                PasswordFrame.BorderColor = Color.Black;
            }
        }
    }
}
