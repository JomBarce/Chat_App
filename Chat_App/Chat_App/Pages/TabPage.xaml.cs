using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabPage : TabbedPage
    {
        DataClass dataClass = DataClass.GetInstance;
        public TabPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            profilePage.Name = dataClass.loggedInUser.name;
            profilePage.Email = dataClass.loggedInUser.email;
        }
    }
}