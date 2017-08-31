using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain.DTO
{
    public class ApartmentCard
    {
        public ApartmentCard() { }

        public ApartmentCard(Apartment apartment)
        {
            ApartmentId = apartment.ApartmentId;
            Type = apartment.Type;
            Price = apartment.Price;
            BedroomCount = apartment.BedroomCount;
            SleepingPlaces = apartment.SleepingPlaces;
            Square = apartment.Square;
            
            Title = apartment.Title;
            Rating = apartment.Rating;
            Description = apartment.Description;
            PostDate = apartment.PostDate.Date;

            City = apartment.City;
            District = apartment.District;
            Street = apartment.Street;
            StreetNumber = apartment.StreetNumber;
            FloorNumber = apartment.FloorNumber;
            UserID = apartment.UserID;
        }

        public int ApartmentId { get; set; }

        public string Title { get; set; }
        public byte Type { get; set; }
        public decimal Price { get; set; }
        public short BedroomCount { get; set; }
        public short SleepingPlaces { get; set; }
        public float Rating { get; set; }
        public string Description { get; set; }
        public float Square { get; set; }
        public DateTime PostDate { get; set; }

        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public short StreetNumber { get; set; }
        public short FloorNumber { get; set; }

        public int UserID { get; set; }

        public Apartment GetApartment(Apartment a)
        {
            a.ApartmentId = ApartmentId;
            a.Price = Price;
            a.BedroomCount = BedroomCount;
            a.SleepingPlaces = SleepingPlaces;
            a.Square = Square;
            a.Title = Title;
            a.Description = Description;
            a.City = City;
            a.District = District;
            a.Street = Street;
            a.StreetNumber = StreetNumber;
            a.FloorNumber = FloorNumber;
            a.UserID = UserID;

            return a;
        }

        public Apartment GetApartment(ApartmentCard a)
        {
            var apartment = new Apartment();

            apartment.ApartmentId = ApartmentId;
            apartment.Type = Type;
            apartment.Price = Price;
            apartment.BedroomCount = BedroomCount;
            apartment.SleepingPlaces = SleepingPlaces;
            apartment.Square = Square;
            apartment.Title = Title;
            apartment.Description = Description;
            apartment.City = City;
            apartment.District = District;
            apartment.Street = Street;
            apartment.StreetNumber = StreetNumber;
            apartment.FloorNumber = FloorNumber;
            apartment.UserID = UserID;

            return apartment;
        }
    }
}