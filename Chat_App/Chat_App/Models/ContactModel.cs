using System;
using System.Collections.Generic;
using System.Text;

namespace Chat_App
{
    public class ContactModel
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
    }
}
