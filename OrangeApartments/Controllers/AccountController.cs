using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using OrangeApartments.Core;
using OrangeApartments.Core.Domain;
using OrangeApartments.Helpers;
using OrangeApartments.Models;

namespace OrangeApartments.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork uof)
        {
            _unitOfWork = uof;
        }

        [Route("login")]
        [HttpPost]
        public HttpResponseMessage Login([FromBody]User model)
        {

            var ecryptedPassword = Encrypt(model.Password);
            User user = _unitOfWork.Users.SingleOrDefault(u => u.Mail == model.Mail && u.Password == ecryptedPassword);
            if (user == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            return Request.CreateResponse(HttpStatusCode.OK, SessionHelper.CreateSession(user.UserId));
        }

        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest); 
            }

            _unitOfWork.Users.Add(new User()
            {
                Name = model.UserName,
                Password = Encrypt(model.Password),
                Mail = model.Email,
                Phone = model.PhoneNumber,
                IsAdmin = false,
                Login = model.Email.Split('@')[0],
                RegistrationDate = DateTime.Now
            });


            _unitOfWork.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("logout")]
        [HttpPost]
        public HttpResponseMessage Logout(RegisterModel model)
        {
            SessionHelper.ClearAllSessions();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
}
