using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConversationPage : ContentPage
    {
        private ObservableCollection<ConversationModel> message;

        public ObservableCollection<ConversationModel> Message
        {
            get { return message; }
            set { message = value; }
        }

        public ConversationPage(ContactModel User)
        {
            InitializeComponent();
            NavTitle.Text = User.Name;

            this.BindingContext = this;
            
            Message = new ObservableCollection<ConversationModel>();
            Message.Add(new ConversationModel() { Id = 1, Message = "Hi", ConverseeID = 1, TimeCreated = DateTime.Now, IsOwner = false});
            Messages.ItemsSource = Message;
        }

        private void Send_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MessageEntry.Text))
            {
                Message.Add(new ConversationModel() { Id = 1, Message = MessageEntry.Text, ConverseeID = 0, TimeCreated = DateTime.Now, IsOwner = true });
                MessageEntry.Text = string.Empty;
            }
            
        }

    }
}