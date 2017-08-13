using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrangeApartments.Core.Domain
{
    public class Tag
    {
        public Tag()
        {
            ApartmentTags = new List<ApartmentTags>();
        }

        public int TagId { get; set; }
        [MaxLength(50)]
        public String TagName { get; set; }

        public virtual ICollection<ApartmentTags> ApartmentTags { get; set; }
    }
}