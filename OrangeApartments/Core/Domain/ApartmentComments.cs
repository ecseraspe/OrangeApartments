using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain
{
    public class ApartmentComments
    {
        public int ApartmentCommentsId { get; set; }
        [MaxLength(500)]
        public string Comment { get; set; }
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-YYYY}", ApplyFormatInEditMode = true)]
        public DateTime CommentDate { get; set; }
        [Required]
        public int ApartmentId { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual Apartment Apartment { get; set; }
    }
}