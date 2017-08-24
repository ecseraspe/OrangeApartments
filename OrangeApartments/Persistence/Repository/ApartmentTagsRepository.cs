using OrangeApartments.Core.Domain;
using OrangeApartments.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OrangeApartments.Persistence.Repository
{
    public class ApartmentTagsRepository : Repository<ApartmentTags>, IApartmentTagsRepository
    {
        public ApartmentTagsRepository(DbContext context) 
            : base(context)
        {
        }

        public ApartmentContext ApartmentContext
        {
            get { return Context as ApartmentContext; }
        }
    }
}