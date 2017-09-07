using OrangeApartments.Core.Domain;
using OrangeApartments.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using OrangeApartments.Core.Domain.DTO;

namespace OrangeApartments.Persistence.Repository
{
    public class ApartmentCommentsRepository : Repository<ApartmentComments>, IApartmentCommentsRepository
    {
        public ApartmentCommentsRepository(DbContext context) 
            : base(context)
        {
        }

        public IEnumerable<ApartmentComentDTO> GetApartmentComents(int apartmentId)
        {
            var coments = Find(x => x.ApartmentId == apartmentId)
                            .OrderByDescending(x => x.CommentDate)
                            .Select(c => new ApartmentComentDTO(c));
            return coments;
        }

        public void AddComment(ApartmentComments coment, int userId)
        {
            coment.UserId = userId;
            Add(coment);
        }

        public ApartmentContext ApartmentContext
        {
            get { return Context as ApartmentContext; }
        }
    }
}