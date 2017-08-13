using OrangeApartments.Core;
using OrangeApartments.Core.Domain;
using OrangeApartments.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrangeApartments.Controllers
{
    public class UserController : ApiController
    {
        private IUnitOfWork _uof;

        public UserController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        // GET: api/User

        public IEnumerable<User> Get()
        {
            using (_uof)
            {
                var users = _uof.Users.GetListOfAdmins();
                return users;
            }
        }

        // GET: api/User/5
        public User Get(int id)
        {
            return _uof.Users.Get(id);
        }

        // POST: api/User
        public void Post([FromBody] User user)
        {
            var content = Request.Content;
            var headers = Request.Headers;
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]User user)
        {
            var date = user.RegistrationDate;
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
