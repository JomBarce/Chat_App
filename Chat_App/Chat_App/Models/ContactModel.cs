using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Chat_App
{
    public class ContactModel : INotifyPropertyChanged
    {
        string Id { get; set; }
        public string id { get { return Id; } set { Id = value; OnPropertyChanged(nameof(id)); } }

        string[] ContactID { get; set; }
        public string[] contactID { get {return ContactID; } set { ContactID = value; OnPropertyChanged(nameof(contactID)); } }

        string[] ContactName { get; set; }
        public string[] contactName { get { return ContactName; } set { ContactName = value; OnPropertyChanged(nameof(contactName)); } }

        string[] ContactEmail { get; set; }
        public string[] contactEmail { get { return ContactEmail; } set { ContactEmail = value; OnPropertyChanged(nameof(contactEmail)); } }

        string Created_at { get; set; }
        public string created_at { get { return Created_at; } set { Created_at = value; OnPropertyChanged(nameof(created_at)); } }

        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
