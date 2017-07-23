using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain
{
    public class ApartmentBooking
    {
        public int ApartmentBookingId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int? UserId { get; set; }
        public int ApartmentId { get; set; }

        public User User { get; set; }
        public Apartment Apartment { get; set; }
    }
}