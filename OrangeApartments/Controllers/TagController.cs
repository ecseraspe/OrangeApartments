using OrangeApartments.Core;
using OrangeApartments.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrangeApartments.Controllers
{
    public class TagController : ApiController
    {
        private IUnitOfWork _uof;

        public TagController(IUnitOfWork _uof)
        {
            this._uof = _uof;
        }

        // GET: api/Tag/getList
        [HttpGet]
        [Route("api/tag/getList")]
        public HttpResponseMessage Get()
        {
            try
            {
                var tags = _uof.Tags.GetAllTags();
                if (tags.Count() == 0)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, tags);
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error during get operation");
            }
        }

        // POST: api/Tag
        [HttpPost]
        [Route("api/tag")]
        public HttpResponseMessage Post([FromBody]string value)
        {
            try
            {
                var tag = new Tag()
                {
                    TagName = value
                };

                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Tag not valid");

                _uof.Tags.Add(tag);
                _uof.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new { tag.TagId, tag.TagName });

            } catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException);
            }
        }

        // PUT: api/Tag/5
        [HttpPut]
        [Route("api/tag/{id}")]
        public HttpResponseMessage Put(int id, [FromBody]string value)
        {
            try
            {
                var tag = _uof.Tags.Get(id);

                if (tag == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "tag with this id not found");

                tag.TagName = value;
                _uof.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, tag);
            }catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // DELETE: api/Tag/5
        [HttpDelete]
        [Route("api/tag/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var tag = _uof.Tags.Get(id);

                if (tag == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Tag not found");

                _uof.Tags.Remove(tag);
                _uof.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
