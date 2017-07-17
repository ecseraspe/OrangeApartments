using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain
{
    public class ApartmentComments
    {
        public int ApartmentCommentsId { get; set; }
        public string Comment { get; set; }
        public string CommentDate { get; set; }

        public Apartment Apartment { get; set; }
        public User User { get; set; }
    }
}