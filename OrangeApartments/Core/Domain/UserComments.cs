using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrangeApartments.Core.Domain
{
    public class UserComments
    {
        public UserComments()
        {
            CommentDate = DateTime.Now;
        }
        public int UserCommentsId { get; set; }
        [MaxLength(500)]
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }

        public virtual User Commentator { get; set; }   // comment writer
        public virtual User CommentedUser { get; set; } // receiver of comment
    }
}