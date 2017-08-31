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
using System.Data.Entity.Core.Objects;
using System.Linq.Expressions;
using System.Text;

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

        [Route("api/apartment")]
        public IEnumerable<ApartmentCard> Get(string city = "", string district = "", string street = "", string sortBy = "price", int page = 0)
        {
            // Search expression
            Expression<Func<Apartment, bool>> er = (arg) =>
                                ((city == "") ? arg.City != null : arg.City == city)
                                && ((district != "") ? arg.District == district : arg.District != null)
                                && ((street != "") ? arg.Street == street : arg.Street != null);

            return _uof.Apartments.GetApartmentsPaging(er, sortBy, page);
        }

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

            // Return default image if no images are stored for current apartment
            // and if image with index 0 requested
            if (imageIndex == 0)
            {
                if (images.Length == 0)
                {
                    string fileName = string.Format("{0}{1}.jpg", System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/Img/"), "dafault-apartment");
                    if (!File.Exists(fileName))
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    
                    FileStream fileStream = File.OpenRead(fileName);
                    HttpResponseMessage response = new HttpResponseMessage
                    {
                        Content = new StreamContent(fileStream),
                        StatusCode = HttpStatusCode.OK
                    };
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    response.Content.Headers.ContentLength = fileStream.Length;
                    return response;
                }
            }

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
        public async Task<HttpResponseMessage> PostImage(int apartmentId)
        {
            // Check if the request contains multipart/form-data. 
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string tmpFileRoot = HttpContext.Current.Server.MapPath("~/App_Data/Img/Apartments/tmp");
            var provider = new MultipartFormDataStreamProvider(tmpFileRoot);
            
            try
            {
                StringBuilder sb = new StringBuilder(); // Holds the response body 
                await Request.Content.ReadAsMultipartAsync(provider);

                IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                foreach (var file in provider.FileData)
                {
                    FileInfo fileInfo = new FileInfo(file.LocalFileName);

                    var fileExt = file.Headers.ContentDisposition.FileName.Substring(file.Headers.ContentDisposition.FileName.LastIndexOf('.')).ToLower();
                    fileExt = fileExt.Substring(0,fileExt.Length-1);
                    if (fileInfo.Length > 5242880)
                    {
                        fileInfo.Delete();
                        sb.Append(string.Format("File: {0} size is to large. ({1} bytes)\n", file.Headers.ContentDisposition.FileName, fileInfo.Length));
                    }
                    else if(!AllowedFileExtensions.Contains(fileExt))             // verify file extension
                    {
                        fileInfo.Delete();
                        sb.Append(string.Format("File: {0} has not alowed extension {1}. Please use: .jpg, .gif, .png.\n", file.Headers.ContentDisposition.FileName, fileExt));
                    }
                    else
                    {
                        var storagePath = HttpContext.Current.Server.MapPath("~/App_Data/Img/Apartments/");
                        string[] files = Directory.GetFiles(storagePath, apartmentId.ToString() + "_*.*", SearchOption.TopDirectoryOnly);

                        string posteFileHash;
                        using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                        {
                            var tmp = File.OpenRead(file.LocalFileName);
                            posteFileHash = Convert.ToBase64String(sha1.ComputeHash(tmp));
                            tmp.Dispose();
                        }

                        bool fileAlreadyExsists = false;
                        if (files.Length > 0)
                            foreach (string tmpFile in files)
                            {
                                string localFileHash;
                                using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                                {
                                    var tmp = File.OpenRead(tmpFile);
                                    localFileHash = Convert.ToBase64String(sha1.ComputeHash(tmp));
                                    tmp.Dispose();
                                }

                                if (localFileHash == posteFileHash)
                                    fileAlreadyExsists = true;
                            }

                        if (fileAlreadyExsists == false)
                        {
                            storagePath = HttpContext.Current.Server.MapPath("~/App_Data/Img/Apartments/" + apartmentId.ToString() + "_" + DateTime.Now.ToFileTime().ToString() + fileExt);
                            File.Move(file.LocalFileName, storagePath);
                        }
                        else
                        {
                            fileInfo.Delete();
                        }
                    }
                }
                return new HttpResponseMessage()
                {
                    Content = new StringContent(sb.ToString())
                };
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
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
