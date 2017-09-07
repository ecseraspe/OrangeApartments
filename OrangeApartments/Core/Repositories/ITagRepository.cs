using OrangeApartments.Core.Domain;
using System.Collections.Generic;

namespace OrangeApartments.Core.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        IEnumerable<Tag> GetAllTags();
        IEnumerable<Tag> GetTags(int apartmentId);
    }
}
