using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain.DTO
{
    public class ApartmentComentDTO
    {
        public ApartmentComentDTO() { }

        public ApartmentComentDTO(ApartmentComments coment)
        {
            Comment = coment.Comment;
            CommentDate = coment.CommentDate;
            UserId = coment.UserId;
        }

        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
        public int? UserId { get; set; }
    }
}