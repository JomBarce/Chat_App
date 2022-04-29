using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabPage : TabbedPage
    {
        public TabPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            /*
            profilePage.Name = dataClass.loggedInUser.name;
            profilePage.Email = dataClass.loggedInUser.email;
            */
        }
    }
}