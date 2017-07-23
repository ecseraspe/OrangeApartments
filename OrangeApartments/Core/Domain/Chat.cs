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
        public DateTime MessageDate { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}