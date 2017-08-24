using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrangeApartments.Core.Domain
{
    public class Apartment
    {
        public Apartment()
        {
            Comments = new List<ApartmentComments>();
            Bookings = new List<ApartmentBooking>();
            ApartmentWatchList = new List<UserWatchList>();
            Tags = new List<ApartmentTags>();

            PostDate = DateTime.Now;
        }

        public int ApartmentId { get; set; }
        [Range((byte)0, (byte)1)]
        public byte Type { get; set; }
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }
        [Range((short)1, (short)20)]
        public short BedroomCount { get; set; }
        [Range((short)1, (short)20)]
        public short SleepingPlaces { get; set; }
        [MaxLength(20)]
        public string Title { get; set; }
        [Range(0, 5)]
        public float Rating { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Range((short)0, (short)1)]
        public short RentType { get; set; }
        [Range(5.0, 5000.0)]
        public float Square { get; set; }
        public DateTime PostDate { get; private set; }
        [MinLength(3), MaxLength(50)]
        public string City { get; set; }
        [MinLength(3), MaxLength(50)]
        public string District { get; set; }
        [MinLength(3), MaxLength(50)]
        public string Street { get; set; }
        [Range((short)1, (short)2000)]
        public short StreetNumber { get; set; }
        [Range(1, 10000)]
        public short FloorNumber { get; set; }
        [Required]
        public int UserID { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ApartmentComments> Comments { get; set; }
        public virtual ICollection<ApartmentBooking> Bookings { get; set; }
        public virtual ICollection<UserWatchList> ApartmentWatchList { get; set; }
        public virtual ICollection<ApartmentTags> Tags { get; set; }
}
}