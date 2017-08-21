using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrangeApartments.Core;
using OrangeApartments.Core.Domain;
using OrangeApartments.Core.Domain.DTO;

namespace OrangeApartments.Controllers
{
    public class ApartmentController : ApiController
    {
        // GET: api/Apartment
        private IUnitOfWork _uof;

        public ApartmentController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public IEnumerable<ApartmentCard> Get()
        {
            var data = _uof.Apartments.GetSelectedApartments(4);
            var data2 = _uof.Apartments.GetApartmentsOfUser(4);
            
            return data;
        }

        // GET: api/Apartment/5
        public Apartment Get(int id)
        {
            return _uof.Apartments.Get(id);
        }

        // POST: api/Apartment
        public void Post([FromBody]Apartment apart)
        {
            using (_uof)
            {
                _uof.Apartments.Add(apart);
                _uof.SaveChanges();
            }
        }

        // PUT: api/Apartment/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Apartment/5
        public void Delete(int id)
        {
        }
    }
}
