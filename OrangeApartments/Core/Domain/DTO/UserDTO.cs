using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain.DTO
{
    public class UserDTO
    {
        public UserDTO() { }

        public UserDTO(User user)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Phone = user.Phone;
            RegistrationDate = user.RegistrationDate;
            Mail = user.Mail;
            AboutMe = user.AboutMe;
        }

        public int UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public string Mail { get; private set; }
        public string AboutMe { get; private set; }
    }
}