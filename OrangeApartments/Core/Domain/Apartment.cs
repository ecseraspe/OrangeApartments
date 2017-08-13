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
        public Apartment()
        {
            Comments = new List<ApartmentComments>();
            Bookings = new List<ApartmentBooking>();
            ApartmentWatchList = new List<UserWatchList>();
            Tags = new List<ApartmentTags>();

            //Default values setting
            Price = 0;
            Title = "";
            Type = 0;
            BedroomCount = 1;
            SleepingPlaces = 1;
            Rating = 0;
            RentType = 1;
        }

        public int ApartmentId { get; set; }
        public byte Type { get; set; }
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }
        public ushort BedroomCount { get; set; }
        public ushort SleepingPlaces { get; set; }
        [MaxLength(20)]
        public string Title { get; set; }
        public float Rating { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public ushort RentType { get; set; }
        public float Square { get; set; }
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-YYYY}", ApplyFormatInEditMode = true)]
        public DateTime PostDate { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string District { get; set; }
        [MaxLength(50)]
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