using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrangeApartments.Core.Domain
{
    public class Tag
    {
        public Tag()
        {
            ApartmentTags = new List<ApartmentTags>();
        }

        public int TagId { get; set; }

        [Required(ErrorMessage = "Tag cannot be empty")]
        [Index(IsUnique = true)]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public String TagName { get; set; }

        public virtual ICollection<ApartmentTags> ApartmentTags { get; set; }
    }
}