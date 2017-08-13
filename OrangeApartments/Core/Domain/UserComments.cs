using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrangeApartments.Core.Domain
{
    public class UserComments
    {
        public int UserCommentsId { get; set; }
        [MaxLength(500)]
        public string Comment { get; set; }
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-YYYY}", ApplyFormatInEditMode = true)]
        public DateTime CommentDate { get; set; }

        public virtual User Commentator { get; set; }   // comment writer
        public virtual User CommentedUser { get; set; } // receiver of comment
    }
}