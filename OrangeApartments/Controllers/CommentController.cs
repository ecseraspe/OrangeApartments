using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using OrangeApartments.Core;
using OrangeApartments.Core.Domain;

namespace OrangeApartments.Controllers
{
    [RoutePrefix("api/comment")]
    public class CommentController : ApiController
    {
        private IUnitOfWork _uof;

        public CommentController(IUnitOfWork _uof)
        {
            this._uof = _uof;
        }


        public class Comm
        {
            public string Comment { get; set; }
        }

        [HttpPost]
        [Route("{commentedUserId}/write-comment/{commentatorId}")]
        public HttpResponseMessage WriteComment(int commentedUserId, int commentatorId, [FromBody]Comm comment)
        {

;            _uof.UserComments.Add(new UserComments()
            {
                Comment = comment.Comment,
                CommentatorId = commentatorId,
                CommentedUserId = commentedUserId,
            });
            _uof.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, _uof.UserComments.GetAll());
        }

        [HttpGet]
        [Route("{commentedUserId}")]
        public HttpResponseMessage GetCommentedUserComments(int commentedUserId)
        {
            //var usrs = _uof.Users.Get(commentedUserId).CommentedUsers.ToList();

            var userProfileComments = _uof.UserComments.Find(c=>c.CommentedUserId == commentedUserId).ToList();
         

            return Request.CreateResponse(HttpStatusCode.OK, userProfileComments);
        }
    }
}