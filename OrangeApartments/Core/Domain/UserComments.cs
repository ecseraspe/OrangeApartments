using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrangeApartments.Core.Domain
{
    public class UserComments
    {
        public int UserCommentsId { get; set; }
        public string Comment { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CommentDate { get; set; }

        public virtual User Commentator { get; set; }   // comment writer
        public virtual User CommentedUser { get; set; } // receiver of comment
    }
}