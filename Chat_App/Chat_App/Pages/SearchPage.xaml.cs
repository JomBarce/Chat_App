using Newtonsoft.Json;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Chat_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        private ObservableCollection<UserModel> result = new ObservableCollection<UserModel>();

        DataClass dataClass = DataClass.GetInstance;

        private bool connected;

        [Obsolete]
        public SearchPage(string user)
        {
            InitializeComponent();
            SearchUser(user);
        }

        [Obsolete]
        private async void SearchUser(string param)
        {
            var documents = await CrossCloudFirestore.Current.Instance.GetCollection("users")
                                                     .WhereEqualsTo("email", param)
                                                     .GetDocumentsAsync();
            foreach (var documentChange in documents.DocumentChanges)
            {
                var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                UserModel obj = JsonConvert.DeserializeObject<UserModel>(json);

                result.Add(obj);
            }
            resultList.ItemsSource = result;

            if (result.Count == 0)
            {
                await DisplayAlert("", "User not found.", "Okay");
                await Navigation.PopAsync(true);
            }
        }

        [Obsolete]
        private async void Result_Clicked(object sender, ItemTappedEventArgs e)
        {
            UserModel item = e.Item as UserModel;

            bool retryBool = await DisplayAlert("Add contact", "Would you like to add " + item.name + "?", "Yes", "No");

            if (dataClass.loggedInUser.uid == item.uid && retryBool)
            {
                await DisplayAlert("Error", "Cannot add self to contacts.", "Okay");
            }
            else if(dataClass.loggedInUser.uid != item.uid && retryBool)
            {
                ContactModel contact = new ContactModel()
                {
                    id = Convert.ToBase64String(BitConverter.GetBytes(DateTime.Now.Ticks)).Replace('+', '_').Replace('/', '-').TrimEnd('='),
                    contactID = new string[] { DataClass.GetInstance.loggedInUser.uid, item.uid },
                    contactEmail = new string[] { DataClass.GetInstance.loggedInUser.email, item.email },
                    contactName = new string[] { DataClass.GetInstance.loggedInUser.name, item.name },
                    created_at = DateTime.UtcNow.ToString("yyyy-MM-dd-HH:mm:ss")
                };

                connected = await IsConnected(item);
                if (connected == false)
                {
                    await CrossCloudFirestore.Current.Instance.GetCollection("contacts").GetDocument(contact.id).SetDataAsync(contact);

                    if (dataClass.loggedInUser.contacts == null)
                        dataClass.loggedInUser.contacts = new List<string>();
                    dataClass.loggedInUser.contacts.Add(item.uid);
                    await CrossCloudFirestore.Current.Instance.GetCollection("users").GetDocument(dataClass.loggedInUser.uid)
                                             .UpdateDataAsync(new { contacts = dataClass.loggedInUser.contacts });

                    if (item.contacts == null)
                        item.contacts = new List<string>();
                    item.contacts.Add(dataClass.loggedInUser.uid);
                    await CrossCloudFirestore.Current.Instance.GetCollection("users").GetDocument(item.uid)
                                             .UpdateDataAsync(new { contacts = item.contacts });
                    await DisplayAlert("Success", "Contact added!", "Okay");
                }
                else
                {
                    await DisplayAlert("Failed", "You both already have a connection", "Okay");
                }
            }
        }

        [Obsolete]
        private async Task<bool> IsConnected(UserModel user)
        {
            var document = await CrossCloudFirestore.Current.Instance.GetCollection("users").GetDocument(dataClass.loggedInUser.uid)
                                              .GetDocumentAsync();
            var yourModel = document.ToObject<UserModel>();
            bool retVal = yourModel.contacts.Exists(x => x == user.uid);
            return retVal;
        }
    }
}