using System;
using System.Collections.Generic;

namespace OrangeApartments.Core.Domain
{
    public class Tag
    {
        public int TagId { get; set; }
        public String TagName { get; set; }

        public virtual ICollection<ApartmentTags> ApartmentTags { get; set; }
    }
}