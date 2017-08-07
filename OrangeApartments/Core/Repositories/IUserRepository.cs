using System.Collections.Generic;
using OrangeApartments.Core.Domain;

namespace OrangeApartments.Core.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        IEnumerable<User> GetListOfAdmins();
    }
}