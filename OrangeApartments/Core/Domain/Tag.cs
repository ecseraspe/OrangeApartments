using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain
{
    public class Tag
    {
        public int TagId { get; set; }
        public String TagName { get; set; }

        public List<ApartmentTags> ApartmentTags { get; set; }
    }
}