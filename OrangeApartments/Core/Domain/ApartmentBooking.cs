using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain
{
    public class ApartmentBooking
    {
        public int ApartmentBookingId { get; set; }
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-YYYY}", ApplyFormatInEditMode = true)]
        public DateTime DateFrom { get; set; }
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-YYYY}", ApplyFormatInEditMode = true)]
        public DateTime DateTo { get; set; }
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-YYYY}", ApplyFormatInEditMode = true)]
        public DateTime DateBooked { get; set; }
        public int? UserId { get; set; }
        public int ApartmentId { get; set; }

        public virtual User User { get; set; }
        public virtual Apartment Apartment { get; set; }
    }
}