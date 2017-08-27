using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrangeApartments.Core;
using OrangeApartments.Core.Domain;
using OrangeApartments.Core.Domain.DTO;
using System.Web;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Net.Http.Headers;

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

        //public IEnumerable<ApartmentCard> Get()
        //{

        //}

        // GET: api/Apartment/5
        [Route("api/apartment/{apartmentId}")]
        public ApartmentCard Get(int apartmentId)
        {
            using (_uof)
            {
                return _uof.Apartments.GetApartmentDetailedById(apartmentId);
            }
        }

        // GET: api/Apartment/5/img/0
        [Route("api/apartment/{apartmentId}/img/{imageIndex}")]
        public HttpResponseMessage GetApartmentImage(int apartmentId, int imageIndex)
        {
            var filePath = HttpContext.Current.Server.MapPath("~/App_Data/Img/Apartments/");
            string[] images = Directory.GetFiles(filePath, apartmentId.ToString() + "_*.*", SearchOption.TopDirectoryOnly);
            if ((images.Length > 0) && (imageIndex < images.Length))
                if (File.Exists(images[imageIndex]))
                {
                    FileStream fileStream = File.OpenRead(images[imageIndex]);
                    HttpResponseMessage response = new HttpResponseMessage
                    {
                        Content = new StreamContent(fileStream),
                        StatusCode = HttpStatusCode.OK
                    };
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    response.Content.Headers.ContentLength = fileStream.Length;
                    return response;
                }

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Saves Appartment images if App_Data/Img/Apartments folder
        /// Image naming convention: apartmentId_DateTime.extension
        /// 
        /// Uses hashing to prevent duplicate images.
        /// </summary>
        /// <param name="apartmentId"></param>
        /// <returns></returns>
        /// 
        // TODO Make file to store image hashesh for fater method execution
        // POST: api/Apartment/5/SaveImg
        [Route("api/apartment/{apartmentId}/SaveImg")]
        public HttpResponseMessage PostImage(int apartmentId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            if (_uof.Apartments.GetApartmentDetailedById(apartmentId) == null)
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed);

            try
            {
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 5; //Size = 4 MB  

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
                            var storagePath = HttpContext.Current.Server.MapPath("~/App_Data/Img/Apartments/");
                            string[] files = Directory.GetFiles(storagePath, apartmentId.ToString() + "_*.*", SearchOption.TopDirectoryOnly);

                            string posteFileHash;
                            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                                posteFileHash = Convert.ToBase64String(sha1.ComputeHash(postedFile.InputStream));

                            bool fileAlreadyExsists = false;
                            if (files.Length > 0)
                                foreach (string tmpFile in files)
                                {
                                    string localFileHash;
                                    using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                                        localFileHash = Convert.ToBase64String(sha1.ComputeHash(File.OpenRead(tmpFile)));

                                    if (localFileHash == posteFileHash)
                                        fileAlreadyExsists = true;
                                }

                            if (fileAlreadyExsists == false)
                            {
                                storagePath = HttpContext.Current.Server.MapPath("~/App_Data/Img/Apartments/" + apartmentId.ToString() + "_" + DateTime.Now.ToFileTime().ToString() + extension);
                                postedFile.SaveAs(storagePath);
                            }
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                dict.Add("error", string.Format("Error during Image Uploading"));
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }

        }


        // POST: api/Apartment
        [Route("api/apartment")]
        public void Post([FromBody]ApartmentCard apart)
        {
            using (_uof)
            {
                _uof.Apartments.Add(apart.GetApartment(apart));
                _uof.SaveChanges();
            }
        }

        // PUT: api/Apartment/5
        [Route("api/apartment/{id}")]
        public void Put(int id, [FromBody]Apartment value)
        {
            using (_uof)
            {
                var apart = _uof.Apartments.Get(id);
                apart = value;
                _uof.SaveChanges();
            }
        }

        // DELETE: api/Apartment/5
        public void Delete(int id)
        {
            using (_uof)
            {
                var apartment = _uof.Apartments.Get(id);
                if (apartment == null)
                    return;

                _uof.Apartments.Remove(apartment);
            }
        }
    }
}
