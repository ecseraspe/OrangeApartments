using OrangeApartments.Core.Domain;
using OrangeApartments.Core.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrangeApartments.Core.Repositories
{
    public interface IApartmentRepository: IRepository<Apartment>
    {
        IEnumerable<ApartmentCard> GetApartmentsOfUser(int userId);
        IEnumerable<ApartmentCard> GetApartmentsPaging(Expression<Func<Apartment, bool>> predicate, string sortBy, int page, string tags);
        IEnumerable<ApartmentCard> GetLikedApartments(int userId);
        ApartmentCard GetApartmentDetailedById(int apartmentId);
        ApartmentCard UpdateApartment(int apartmentId, ApartmentCard apartmentData);
    }
}
