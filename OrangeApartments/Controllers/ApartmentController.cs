﻿using System;
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
        public HttpResponseMessage Get(string city = "", string district = "", string street = "", string sortBy = "price", int page = 0)
        {
            try
            {
                // Search expression
                Expression<Func<Apartment, bool>> er = (arg) =>
                                    ((city == "") ? arg.City != null : arg.City == city)
                                    && ((district != "") ? arg.District == district : arg.District != null)
                                    && ((street != "") ? arg.Street == street : arg.Street != null);

                var apartmentList = _uof.Apartments.GetApartmentsPaging(er, sortBy, page);

                return Request.CreateResponse(HttpStatusCode.OK, apartmentList);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured during search operation");
            }
        }

        // GET: api/Apartment/5
        [Route("api/apartment/{apartmentId}")]
        public HttpResponseMessage Get(int apartmentId)
        {
            try
            {
                var apartment = _uof.Apartments.GetApartmentDetailedById(apartmentId);

                if (apartment == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent, "Apartment not found");

                return Request.CreateResponse(HttpStatusCode.OK, apartment);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error during apartment search");
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
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [Route("api/apartment/{id}/delImage/{imgId}")]
        public HttpResponseMessage DeleteImage(int id, int imgId)
        {
            try
            {
                var storagePath = HttpContext.Current.Server.MapPath("~/App_Data/Img/Apartments/");
                string[] files = Directory.GetFiles(storagePath, id.ToString() + "_*.*", SearchOption.TopDirectoryOnly);

                // image exsists -> delte it
                if (files.Length > 0 &&  imgId < files.Length)
                {
                    File.Delete(files[imgId]);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Image not found");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error during image delete process");
            }
        }


        // POST: api/Apartment
        [Route("api/apartment")]
        public HttpResponseMessage Post([FromBody]ApartmentCard apart)
        {
            try
            {
                var apartment = apart.GetApartment(apart);
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Model is not valid");

                _uof.Apartments.Add(apartment);
                _uof.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, new ApartmentCard(apartment));
                message.Headers.Location = new Uri(Request.RequestUri + apartment.ApartmentId.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error during new apartment post process");
            }

        }

        // PUT: api/Apartment/5
        [Route("api/apartment/{id}")]
        public HttpResponseMessage Put(int id, [FromBody]ApartmentCard value)
        {
            try
            {
                var apartment = _uof.Apartments.Get(id);
                if (apartment == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Apartment was not found");

                apartment = value.GetApartment(apartment);
                _uof.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, value);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error during apartment update process");
            }
        }

        // DELETE: api/Apartment/5
        [Route("api/apartment/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var apartment = _uof.Apartments.Get(id);
                if (apartment == null)
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, "Apartment not found");

                _uof.Apartments.Remove(apartment);
                _uof.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error during apartment delete process");
            }
        }
    }
}
