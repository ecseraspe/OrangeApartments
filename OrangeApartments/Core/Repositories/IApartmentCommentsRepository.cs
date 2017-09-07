using OrangeApartments.Core.Domain;
using OrangeApartments.Core.Domain.DTO;
using System.Collections.Generic;

namespace OrangeApartments.Core.Repositories
{
    public interface IApartmentCommentsRepository : IRepository<ApartmentComments>
    {
        IEnumerable<ApartmentComentDTO> GetApartmentComents(int apartmentID);
        void AddComment(ApartmentComments coment, int userId);
    }
}
