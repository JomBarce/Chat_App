using Newtonsoft.Json;
using Plugin.CloudFirestore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        private ObservableCollection<ContactModel> contactList = new ObservableCollection<ContactModel>();

        DataClass dataClass = DataClass.GetInstance;

        [Obsolete]
        public ChatPage()
        { 
            InitializeComponent();
            contactsList.ItemsSource = contactList;
            GetContacts();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        [Obsolete]
        private void GetContacts()
        {
            CrossCloudFirestore.Current.Instance.GetCollection("contacts")
                .WhereArrayContains("contactID", dataClass.loggedInUser.uid)
                .AddSnapshotListener((snapshot, error) =>
                {
                    loading.IsVisible = true;
                    if (snapshot != null)
                    {
                        foreach (var documentChange in snapshot.DocumentChanges)
                        {
                            var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                            var obj = JsonConvert.DeserializeObject<ContactModel>(json);
                            switch (documentChange.Type)
                            {
                                case DocumentChangeType.Added:
                                    contactList.Add(obj);
                                    break;
                                case DocumentChangeType.Modified:
                                    if (contactList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = contactList.Where(c => c.id == obj.id).FirstOrDefault();
                                        item = obj;
                                    }
                                    break;
                                case DocumentChangeType.Removed:
                                    if (contactList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = contactList.Where(c => c.id == obj.id).FirstOrDefault();
                                        contactList.Remove(obj);
                                    }
                                    break;
                            }
                        }
                    }
                    emptyListLabel.IsVisible = contactList.Count == 0;
                    contactsList.IsVisible = !(contactList.Count == 0);
                    loading.IsVisible = false;
                });
        }

        [Obsolete]
        private async void Search_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchEntry.Text))
            {
                await DisplayAlert("Error", "Missing field/s.", "Okay");
            }
            else
            {
                await Navigation.PushAsync(new SearchPage(SearchEntry.Text), true);
                SearchEntry.Text = string.Empty;
            }
        }

        [Obsolete]
        async private void Contact_Clicked(object sender, ItemTappedEventArgs e)
        {
            ContactModel selectedItem = e.Item as ContactModel;
            await Navigation.PushAsync(new ConversationPage(selectedItem), true);
        }
    }
}