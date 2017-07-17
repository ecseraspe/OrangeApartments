using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain
{
    public class Apartment
    {
        public int ApartmentId { get; set; }
        public byte Type { get; set; }
        public uint Price { get; set; }
        public ushort BedroomCount { get; set; }
        public ushort SleepingPlaces { get; set; }
        public string Title { get; set; }
        public float Rating { get; set; }
        public string Description { get; set; }
        public ushort RentType { get; set; }
        public float Square { get; set; }
        public string PostDate { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public uint StreetNumber { get; set; }
        public ushort FloorNumber { get; set; }

        public User User { get; set; }

        [ForeignKey("Apartment")]
        public List<ApartmentComments> Comments { get; set; }

        [ForeignKey("Apartment")]
        public List<ApartmentBooking> Bookings { get; set; }

        [ForeignKey("Apartment")]
        public List<UserWatchList> ApartmentWatchList { get; set; }

        [ForeignKey("Apartment")]
        public List<ApartmentTags> Tags { get; set; }
    }
}