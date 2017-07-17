using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrangeApartments.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrangeApartments.Core.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public bool IsAdmin { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public List<Apartment> Apartments;

        [ForeignKey("User")]
        public List<UserComments> UserCommnets { get; set; }

        [ForeignKey("User")]
        public List<ApartmentComments> ApartmentCommnets { get; set; }

        [ForeignKey("Sender")]
        public List<Chat> ChatAsSender { get; set; }

        [ForeignKey("Receiver")]
        public List<Chat> ChatAsReceiver { get; set; }

        [ForeignKey("User")]
        public List<ApartmentBooking> Bookings { get; set; }

        [ForeignKey("User")]
        public List<UserWatchList> WatchList { get; set; }
    }
}