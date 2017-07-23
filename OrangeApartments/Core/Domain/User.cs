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
        public DateTime RegistrationDate { get; set; }
        public string Mail { get; set; }
        public bool IsAdmin { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual List<Apartment> Apartments { get; set; }
        public virtual List<ApartmentComments> ApartmentCommnets { get; set; }

        // List of users whom this user wrote the comments
        [InverseProperty("Commentator")]
        public List<UserComments> UserCommnets { get; set; }

        // Users who are commented by current user
        [InverseProperty("CommentedUser")]
        public List<UserComments> CommentedUsers { get; set; }

        [InverseProperty("Sender")]
        public List<Chat> ChatAsSender { get; set; }

        [InverseProperty("Receiver")]
        public List<Chat> ChatAsReceiver { get; set; }

        public List<ApartmentBooking> Bookings { get; set; }

        public List<UserWatchList> WatchList { get; set; }
    }
}