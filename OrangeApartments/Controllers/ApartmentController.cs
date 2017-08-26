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
        [HttpGet]
        [Route("api/apartment/{apartmentId}/GetApartmentDetails")]
        public ApartmentCard Get(int apartmentId)
        {
            using (_uof)
            {
                return _uof.Apartments.GetApartmentDetailedById(apartmentId);
            }
        }

        // GET: api/Apartment/5/GetApartmentImage/2
        [HttpGet]
        [Route("api/apartment/{apartmentId}/GetApartmentImage/{imageIndex}")]
        public HttpResponseMessage GetApartmentImage(int apartmentId, int imageIndex)
        {
            string fileName = string.Format("{0}{1}.png", System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Img/Apartments/"), apartmentId.ToString());

            var filePath = HttpContext.Current.Server.MapPath("~/App_Data/Img/Apartments/");
            string[] images = Directory.GetFiles(filePath, apartmentId.ToString() + "_*.*", System.IO.SearchOption.TopDirectoryOnly);
            if ((images.Length > 0) && (imageIndex < images.Length))
                if (File.Exists(images[imageIndex]))
                {
                    FileStream fileStream = File.OpenRead(fileName);
                    HttpResponseMessage response = new HttpResponseMessage { Content = new StreamContent(fileStream) };
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    response.Content.Headers.ContentLength = fileStream.Length;
                    return response;
                }

            throw new HttpResponseException(HttpStatusCode.NotFound);
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
        [HttpPost]
        [Route("api/apartment/{apartmentId}/SaveApartmentImg")]
        public HttpResponseMessage PostImage(int apartmentId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            if (_uof.Apartments.GetApartmentDetailedById(apartmentId) == null)
            {

            }

            try
            {
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
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
                            string hashOfIncomeFile;
                            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                            {
                                hashOfIncomeFile = Convert.ToBase64String(sha1.ComputeHash(postedFile.InputStream));
                            }

                            var filePath = HttpContext.Current.Server.MapPath("~/App_Data/Img/Apartments/");
                            string[] files = Directory.GetFiles(filePath, apartmentId.ToString() + "_*.*", SearchOption.TopDirectoryOnly);
                            if (files.Length > 0)
                                foreach (string tmpFile in files)
                                {
                                    string hash;
                                    using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                                    {
                                        var fileArr = File.OpenRead(tmpFile);
                                        hash = Convert.ToBase64String(sha1.ComputeHash(fileArr));
                                        fileArr.Dispose();
                                    }
                                    if (hash == hashOfIncomeFile)
                                        File.Delete(tmpFile);
                                }

                            filePath = HttpContext.Current.Server.MapPath("~/App_Data/Img/Apartments/" + apartmentId.ToString() + "_" + DateTime.Now.ToFileTime().ToString() + extension);
                            if (File.Exists(filePath))
                                File.Delete(filePath);
                            postedFile.SaveAs(filePath);
                        }
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


        // POST: api/Apartment
        public void Post([FromBody]Apartment apart)
        {

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
