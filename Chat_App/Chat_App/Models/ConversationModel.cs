using System;
using System.Collections.Generic;
using System.Text;

namespace Chat_App
{
    public class ConversationModel
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        
        public int converseeID;
        public int ConverseeID
        {
            get { return converseeID; }
            set { converseeID = value; }
        }

        private DateTime timeCreated;
        public DateTime TimeCreated
        {
            get{ return timeCreated; }
            set{ timeCreated = value; }
        }

        private bool isOwner;

        public bool IsOwner
        {
            get { return isOwner; }
            set { isOwner = value; }
        }
    }
}
