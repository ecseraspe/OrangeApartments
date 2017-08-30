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
        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            using (_uof)
            {
                var users = _uof.Users.Find(usr => usr.IsAdmin == false);
                return users;
            }
        }

        // GET: api/User/5
        [HttpGet]
        public User GetUser(int id)
        {
            return _uof.Users.Get(id);
        }

        // POST: api/User
        [HttpPost]
        public HttpResponseMessage CreateUser([FromBody]User user)
        {
            _uof.Users.Add(user);
            _uof.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT: api/User/5
        [HttpPut]
        public HttpResponseMessage ChangeUser(int id, [FromBody]User user)
        {
            var date = user.RegistrationDate;
            _uof.Users.Update(user);
            _uof.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE: api/User/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var user = _uof.Users.SingleOrDefault(usr => usr.UserId == id);
            _uof.Users.Remove(user);
            _uof.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

    }
}
