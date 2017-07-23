using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain
{
    public class UserWatchList
    {
        public int UserWatchListId { get; set; }
        public int? UserId { get; set; }
        public int ApartmentId { get; set;}

        public User User { get; set; }
        public Apartment Apartment { get; set; }
    }
}