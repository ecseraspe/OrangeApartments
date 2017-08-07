using System;
using System.Collections.Generic;

namespace OrangeApartments.Core.Domain
{
    public class Tag
    {
        public Tag()
        {
            ApartmentTags = new List<ApartmentTags>();
        }

        public int TagId { get; set; }
        public String TagName { get; set; }

        public virtual ICollection<ApartmentTags> ApartmentTags { get; set; }
    }
}