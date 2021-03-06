﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrangeApartments.Core.Domain
{
    public class User
    {
        public User()
        {
            Apartments = new List<Apartment>();
            ApartmentCommnets = new List<ApartmentComments>();
            //UserCommnets = new List<UserComments>();
            //CommentedUsers = new List<UserComments>();
            ChatAsSender = new List<Chat>();
            ChatAsReceiver = new List<Chat>();
            Bookings = new List<ApartmentBooking>();
            WatchList = new List<UserWatchList>();

            IsAdmin = false;
            RegistrationDate = DateTime.Now;
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Mail { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }
        public string AboutMe { get; set; }

        public virtual List<Apartment> Apartments { get; set; }
        public virtual List<ApartmentComments> ApartmentCommnets { get; set; }

        //List of users whom this user wrote the comments ----- users that I've commented
        //[InverseProperty("Commentator")]
        //public virtual ICollection<UserComments> UserCommnets { get; set; }

        //Users who are commented by current user ------ user who commented me
        //[InverseProperty("CommentedUser")]
        //public virtual ICollection<UserComments> CommentedUsers { get; set; }

        [InverseProperty("Sender")]
        public virtual ICollection<Chat> ChatAsSender { get; set; }
        [InverseProperty("Receiver")]
        public virtual ICollection<Chat> ChatAsReceiver { get; set; }
  
        public virtual ICollection<ApartmentBooking> Bookings { get; set; }

        public virtual ICollection<UserWatchList> WatchList { get; set; }
    }
}