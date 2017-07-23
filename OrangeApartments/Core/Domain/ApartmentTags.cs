using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain
{
    public class ApartmentTags
    {
        public int ApartmentTagsId { get; set; }
        public int TagId { get; set; }
        public int ApartmentId { get; set; }

        public Apartment Apartment { get; set; }
        public Tag Tag { get; set; }
    }
}