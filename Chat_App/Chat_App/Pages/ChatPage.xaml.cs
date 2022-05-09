using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        bool hasContact = true;
        private ObservableCollection<ContactModel> myList;

        public ObservableCollection<ContactModel> MyList
        {
            get { return myList; }
            set { myList = value; }
        }

        public ChatPage()
        { 
            InitializeComponent();
            if (hasContact == false)
            {
                EmptyContact.IsVisible = true;
            }
            else
            {
                EmptyContact.IsVisible = false;
                this.BindingContext = this;
                MyList = new ObservableCollection<ContactModel>();

                for (int i = 1; i < 15; i++)
                {
                    MyList.Add(new ContactModel() { Id = i, Name = "Name LastName" + i.ToString(), Email = "email" + i.ToString() + "@email.com" });
                }

                ContactsList.ItemsSource = MyList;
            }
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

        async private void Contact_Clicked(object sender, ItemTappedEventArgs e)
        {
            ContactModel selectedItem = e.Item as ContactModel;
            await Navigation.PushAsync(new ConversationPage(selectedItem), true);
        }
    }
}