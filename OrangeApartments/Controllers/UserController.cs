using OrangeApartments.Core;
using OrangeApartments.Core.Domain;
using OrangeApartments.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using OrangeApartments.Filters;
using OrangeApartments.Core.Domain.DTO;
using OrangeApartments.Helpers;

namespace OrangeApartments.Controllers
{
    public class UserController : ApiController
    {
        private IUnitOfWork _uof;

        public UserController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        // GET: api/user/{id}
        [HttpGet]
        [Route("api/user/{userId}")]
        public HttpResponseMessage Get(int userId)
        {
            var user = new UserDTO(_uof.Users.Get(userId));
            if (user == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found");

            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        /// <summary>
        /// Get user profile image
        /// 
        /// if user has no image than default image is returned
        /// default image location: App_Data/Img/
        /// default image name: default-user
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        // GET: api/User/{id}/GetUserImg
        //public User Get(int id)
        [HttpGet]
        [Route("api/user/{userId}/GetImg")]
        public HttpResponseMessage GetImg(int userId)
        {
            string fileName = string.Format("{0}{1}.png", System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/Img/Users/"), userId.ToString());
            if (!File.Exists(fileName))
            {
                fileName = string.Format("{0}{1}.png", System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/Img/"), "default-user");
                if (!File.Exists(fileName))
                    throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FileStream fileStream = File.OpenRead(fileName);
            HttpResponseMessage response = new HttpResponseMessage { Content = new StreamContent(fileStream) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            response.Content.Headers.ContentLength = fileStream.Length;
            return response;
        }

        /// <summary>
        /// Add user image
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// TODO: Only authorized user can perform this action.
        [AuthFilter]
        [Route("api/user/{userId}/SaveImg")]
        [HttpPost]
        public HttpResponseMessage SaveImage(int userId)
        {
            var accessTokenValue = Request.Headers.GetValues("Token").FirstOrDefault();
            var currentUserId = SessionHelper.GetSession(accessTokenValue);

            var user = _uof.Users.SingleOrDefault(u => u.UserId == currentUserId);
            if (user == null)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Only authorized users can perform this action");

            if (userId != currentUserId)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Users are allowed to change only their profile image");

            Dictionary<string, object> dict = new Dictionary<string, object>();

            try
            {
                var httpRequest = HttpContext.Current.Request;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                var postedFile = httpRequest.Files[0];
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 4; //Size = 4 MB  

                    IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                    var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                    var extension = ext.ToLower();
                    if (!AllowedFileExtensions.Contains(extension))             // verify file extension
                    {
                        dict.Add("error", string.Format("Please Upload image of type .jpg, .gif, .png."));
                        return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                    }
                    else if (postedFile.ContentLength > MaxContentLength)       // verify file length
                    {
                        dict.Add("error", string.Format("Please Upload a file upto 4 mb."));
                        return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                    }
                    else                                                       // only one img file per user allowed.
                    {
                        // check if user image already exists. If so - delete file to save new.
                        var filePath = HttpContext.Current.Server.MapPath("~/App_Data/Img/Users/");
                        string[] files = System.IO.Directory.GetFiles(filePath, userId.ToString()+".*", System.IO.SearchOption.TopDirectoryOnly);
                        if (files.Length > 0)
                            foreach (string tmpFile in files)
                                File.Delete(tmpFile);

                        filePath = HttpContext.Current.Server.MapPath("~/App_Data/Img/Users/"+userId.ToString()+extension);
                        postedFile.SaveAs(filePath);
                    }
                }

                return Request.CreateErrorResponse(HttpStatusCode.Created, string.Format("Image Updated Successfully."));
            }
            catch (Exception ex)
            {
                dict.Add("error", string.Format("Error during Image Uploading"));
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
		}
    }
}
