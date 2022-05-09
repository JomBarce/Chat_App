using System;
using Xamarin.Forms;

namespace Chat_App
{
    public partial class MainPage : ContentPage
    {
        DataClass dataClass = DataClass.GetInstance;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            EmailEntry.Text = dataClass.loggedInUser != null ? dataClass.loggedInUser.email : "";
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
                FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                res = await DependencyService.Get<iFirebaseAuth>().LoginWithEmailPassword(EmailEntry.Text, PasswordEntry.Text);
                if (res.Status == true)
                {
                    Application.Current.MainPage = new NavigationPage(new TabPage());
                }
                else
                {
                    bool retryBool = await DisplayAlert("Error", res.Response + " Retry?", "Yes", "No");
                    if (retryBool)
                    {
                        EmailEntry.Text = string.Empty;
                        PasswordEntry.Text = string.Empty;
                        EmailEntry.Focus();
                    }
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
