using OrangeApartments.Core.Domain;
using OrangeApartments.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OrangeApartments.Persistence.Repository
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(DbContext context) 
            : base(context)
        {
        }

        public IEnumerable<Tag> GetAllTags()
        {
            var tags = GetAll();
            return tags;
        }

        public ApartmentContext ApartmentContext
        {
            get { return Context as ApartmentContext; }
        }
    }
}