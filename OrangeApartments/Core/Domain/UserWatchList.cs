using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain
{
    public class UserWatchList
    {
        public User User { get; set; }
        public Apartment Apartment { get; set; }
    }
}