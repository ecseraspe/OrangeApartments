using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public byte Type { get; set; }
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }
        public short BedroomCount { get; set; }
        public short SleepingPlaces { get; set; }
        [MaxLength(20)]
        public string Title { get; set; }
        public float Rating { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public short RentType { get; set; }
        public float Square { get; set; }
        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string District { get; set; }
        [MaxLength(50)]
        public string Street { get; set; }
        public int StreetNumber { get; set; }
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