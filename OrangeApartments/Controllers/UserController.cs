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
        //private UnitOfWork _uof;

        //public UserController()
        //{
        //    this._uof = new UnitOfWork(new ApartmentContext());
        //}

        // GET: api/User
        public IEnumerable<User> Get()
        {
            using (var uof = new UnitOfWork(new ApartmentContext()))
            {
                var users = uof.Users.GetListOfAdmins();
                return users;
            }
        }

        // GET: api/User/5
        public User Get(int id)
        {
            using (var uof = new UnitOfWork(new ApartmentContext()))
            {
                return uof.Users.Get(id);
            }
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
