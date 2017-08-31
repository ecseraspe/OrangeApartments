using OrangeApartments.Core.Domain;
using OrangeApartments.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using OrangeApartments.Core.Domain.DTO;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Data.Entity;

namespace OrangeApartments.Persistence.Repository
{
    /// <summary>
    /// Gets Apartment data from DB.
    /// </summary>
    public class ApartmentRepository : Repository<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(ApartmentContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Returns apartments that some user have posted.
        /// </summary>
        /// <param name="userId">id of user whose posted apartments are seached</param>
        /// <returns></returns>
        public IEnumerable<ApartmentCard> GetApartmentsOfUser(int userId)
        {
            return Find(x => x.UserID == userId)
                .Select(x => new ApartmentCard(x));
        }

        /// <summary>
        /// Users have ability to select apartments they have liked.
        /// This method returns list of such apartments.
        /// 
        /// Users cant access other users selected appartments lists,
        /// only own list is accessible.
        /// </summary>
        /// <param name="userId">Id of user whose selected apartments needs to be shown</param>
        /// <returns></returns>
        public IEnumerable<ApartmentCard> GetLikedApartments(int userId)
        {
            var data = ApartmentContext.UserWatchList.Include(uw => uw.Apartment)
                .Where(uw => uw.UserId == userId)
                .Select(c => c.Apartment)
                .ToList()
                .Select(c => new ApartmentCard(c));

            return data;
        }

        /// <summary>
        /// Adds pagging of list of apartments to be able to send data by portions,
        /// not all table from db.
        /// </summary>
        /// <param name="predicate">Search properties. Dont forget to use ORDER BY for correct data output.</param>
        /// <param name="page">Paging starts from 0</param>
        /// <param name="page_size">number of elements to send</param>
        /// <returns></returns>
        public IEnumerable<ApartmentCard> GetApartmentsPaging(Expression<Func<Apartment, bool>> predicate, string sortBy = "Price", int page = 0)
        {
            var res = Find(predicate)
                .OrderBy(sortBy)
                .Skip(page * 5).Take(5)
                .Select(x => new ApartmentCard(x))
                .ToList();

            return res;
        }

        /// <summary>
        /// Returns detailed inforamt
        /// </summary>
        /// <param name="apartmentId"></param>
        /// <returns></returns>
        public ApartmentCard GetApartmentDetailedById(int apartmentId)
        {
            var apartment = Get(apartmentId);

            if (apartment != null)
                return new ApartmentCard(apartment);
            else
                return null;
        }

        public ApartmentCard UpdateApartment(int apartmentId, ApartmentCard apartmentData)
        {
            var apartment = Get(apartmentId);

            if (apartment == null)
                return null;

            apartment = apartmentData.GetApartment(apartment);

            return apartmentData;
        }

        
        /// <summary>
        /// Enables acces to other db tables.
        /// </summary>
        public ApartmentContext ApartmentContext
        {
            get { return Context as ApartmentContext; }
        }
    }
}