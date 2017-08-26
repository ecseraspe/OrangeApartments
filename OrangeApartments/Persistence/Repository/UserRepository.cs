using OrangeApartments.Core.Domain;
using OrangeApartments.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace OrangeApartments.Persistence.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApartmentContext context)
            : base(context)
        {
        }

        public IEnumerable<User> GetListOfAdmins()
        {
            return ApartmentContext.Users
                .Where(u => u.IsAdmin == true)
                .ToList();
        }

        public ApartmentContext ApartmentContext
        {
            get { return Context as ApartmentContext; }
        }
    }
}