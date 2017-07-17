using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain
{
    public class Chat
    {
        public int ChatId { get; set; }
        public string Message { get; set; }
        public string MessageDate { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}