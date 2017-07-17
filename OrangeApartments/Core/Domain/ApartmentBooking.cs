using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain
{
    public class ApartmentBooking
    {
        public int ApartmentBookingId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }

        public User User { get; set; }
        public Apartment Apartment { get; set; }
    }
}