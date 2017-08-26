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
            Name = user.Name;
            Phone = user.Phone;
            RegistrationDate = user.RegistrationDate;
            Mail = user.Mail;
        }

        public int UserId { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public string Mail { get; private set; }
    }
}