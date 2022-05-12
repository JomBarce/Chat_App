using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Chat_App
{ 
    public class UserModel : INotifyPropertyChanged
    {
        string Uid { get; set; }
        public string uid { get { return Uid; } set { Uid = value; OnPropertyChanged(nameof(uid)); } }
        
        string Email { get; set; }
        public string email { get { return Email; } set { Email = value; OnPropertyChanged(nameof(email)); } }

        string Name { get; set; }
        public string name { get { return Name; } set { Name = value; OnPropertyChanged(nameof(name)); } }

        int UserType { get; set; }
        public int userType { get { return UserType; } set { UserType = value; OnPropertyChanged(nameof(userType)); } }

        List<string> Contacts { get; set; }
        public List<string> contacts { get { return Contacts; } set { Contacts = value; OnPropertyChanged(nameof(contacts)); } }

        string Created_at { get; set; }
        public string created_at { get { return Created_at; } set { Created_at = value; OnPropertyChanged(nameof(created_at)); } }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
