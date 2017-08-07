using OrangeApartments.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrangeApartments.Persistence
{
    public class MyDbInitializer : DropCreateDatabaseAlways<ApartmentContext>
    {
        protected override void Seed(ApartmentContext db)
        {
            var User1 = new User
            {
                Name = "Oleg",
                IsAdmin = false,
                Login = "oleg-login",
                Password = "oleg-password",
                Mail = "oleg-mail"
            };
            db.Users.Add(User1);

            var apartment = new Apartment
            {
                City = "Lviv",
                Price = 500,
                UserID = db.Users.FirstOrDefault(p => p.Login == "oleg-login").UserId
            };
            db.Apartments.Add(apartment);

            var apartComment = new ApartmentComments
            {
                Comment = "Apartment1 Comment",
                ApartmentId = db.Apartments.FirstOrDefault(p => p.ApartmentId > 0).ApartmentId,
                UserId = db.Users.FirstOrDefault(p => p.Login == "oleg-login").UserId
            };
            db.SaveChanges();
        }
    }
}