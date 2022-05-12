using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Plugin.CloudFirestore;

namespace Chat_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConversationPage : ContentPage
    {
        private ObservableCollection<ConversationModel> message = new ObservableCollection<ConversationModel>();

        private DataClass dataClass = DataClass.GetInstance;

        private ContactModel contact = new ContactModel();

        [Obsolete]
        public ConversationPage(ContactModel obj)
        {
            InitializeComponent();
            contact = obj;
            NavTitle.Text = (obj.contactID[0] == dataClass.loggedInUser.uid)? obj.contactName[1] : obj.contactName[0];
            GetMessages();
        }

        [Obsolete]
        private void GetMessages()
        {
            CrossCloudFirestore.Current.Instance.GetCollection("contacts").GetDocument(contact.id)
                .GetCollection("conversations").OrderBy("created_at", false)
                .AddSnapshotListener((snapshot, error) =>
                {
                    messagesList.ItemsSource = message;
                    if (snapshot != null)
                    {
                        foreach (var documentChange in snapshot.DocumentChanges)
                        {
                            var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                            var obj = JsonConvert.DeserializeObject<ConversationModel>(json);
                            switch (documentChange.Type)
                            {
                                case DocumentChangeType.Added:
                                    message.Add(obj);
                                    break;
                                case DocumentChangeType.Modified:
                                    if (message.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = message.Where(c => c.id == obj.id).FirstOrDefault();
                                        item = obj;
                                    }
                                    break;
                                case DocumentChangeType.Removed:
                                    if (message.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = message.Where(c => c.id == obj.id).FirstOrDefault();
                                        message.Remove(obj);
                                    }
                                    break;
                            }
                        }
                    }
                    emptyListLabel.IsVisible = message.Count == 0;
                    GoToBottom();
                });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Pan);
        }

        public void GoToBottom()
        {
            if (messagesList.ItemsSource != null)
            {
                var v = messagesList.ItemsSource.Cast<object>().LastOrDefault();
                messagesList.ScrollTo(v, ScrollToPosition.MakeVisible, false);
            }
        }

        public async Task GoToBottomAsync()
        {
            if (messagesList.ItemsSource != null)
            {
                await Task.Delay(500);
                var v = messagesList.ItemsSource.Cast<object>().LastOrDefault();
                messagesList.ScrollTo(v, ScrollToPosition.MakeVisible, false);
            }  
        }

        public void OnFocused(object sender, FocusEventArgs e)
        {
            _ = GoToBottomAsync();
        }

        [Obsolete]
        private async void Send_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MessageEntry.Text))
            {
                string ID = Convert.ToBase64String(BitConverter.GetBytes(DateTime.Now.Ticks)).Replace('+', '_').Replace('/', '-').TrimEnd('=');
                ConversationModel conversation = new ConversationModel()
                {
                    id = ID,
                    converseeID = dataClass.loggedInUser.uid,
                    message = MessageEntry.Text,
                    created_at = DateTime.UtcNow.ToString("yyyy-MM-dd-HH:mm:ss")
                };

                await CrossCloudFirestore.Current.Instance.GetCollection("contacts").GetDocument(contact.id)
                                         .GetCollection("conversations").GetDocument(ID).SetDataAsync(conversation);
            }
            MessageEntry.Text = string.Empty;
            GoToBottom();
        }

    }
}