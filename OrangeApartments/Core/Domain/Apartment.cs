using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public int UserID { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ApartmentComments> Comments { get; set; }
        public virtual ICollection<ApartmentBooking> Bookings { get; set; }
        public virtual ICollection<UserWatchList> ApartmentWatchList { get; set; }
        public virtual ICollection<ApartmentTags> Tags { get; set; }
}
}