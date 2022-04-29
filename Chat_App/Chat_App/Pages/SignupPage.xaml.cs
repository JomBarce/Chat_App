using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }
        private async void SignUp_Clicked(object sender, EventArgs e)
        {
            if ( string.IsNullOrEmpty(NameEntry.Text) || string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text) || string.IsNullOrEmpty(ConfirmPasswordEntry.Text))
            {
                NameFrame.BorderColor = (string.IsNullOrEmpty(NameEntry.Text)) ? Color.Red :Color.Black;
                EmailFrame.BorderColor = (string.IsNullOrEmpty(EmailEntry.Text)) ? Color.Red : Color.Black;
                PasswordFrame.BorderColor = (string.IsNullOrEmpty(PasswordEntry.Text)) ? Color.Red : Color.Black;
                ConfirmPasswordFrame.BorderColor = (string.IsNullOrEmpty(ConfirmPasswordEntry.Text)) ? Color.Red : Color.Black;
                await DisplayAlert("Error", "Missing field/s.", "Okay");
            }
            else
            {
                if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
                {
                    await DisplayAlert("Error", "Passwords don't match.", "Okay");
                    ConfirmPasswordEntry.Text = string.Empty;
                    ConfirmPasswordEntry.Focus();
                }
                else
                {
                    try
                    {
                        await DisplayAlert("Success", "Sign up successful. Verification email sent.", "Okay");
                        await Navigation.PopModalAsync(true);
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", ex.Message, "Okay");
                    }
                }
            }
        }
        private async void SignIn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }

        void TextBox_Focused(object sender, FocusEventArgs e)
        {
            var textBox = sender;
            if (textBox.Equals(NameEntry))
            {
                NameFrame.BorderColor = Color.Black;
            }
            else if (textBox.Equals(EmailEntry))
            {
                EmailFrame.BorderColor = Color.Black;
            }
            else if (textBox.Equals(PasswordEntry))
            {
                PasswordFrame.BorderColor = Color.Black;
            }
            else if (textBox.Equals(ConfirmPasswordEntry))
            {
                ConfirmPasswordFrame.BorderColor = Color.Black;
            }
        }
    }
}