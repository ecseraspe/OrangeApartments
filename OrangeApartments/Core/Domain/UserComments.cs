using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain
{
    public class UserComments
    {
        public int UserCommentsId { get; set; }
        public string Comment { get; set; }
        public string CommentDate { get; set; }

        public User Commentator { get; set; }
        public User User { get; set; }
    }
}