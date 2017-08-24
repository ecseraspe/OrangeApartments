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
            Price = apartment.Price;
            Type = apartment.Type;
            BedroomCount = apartment.BedroomCount;
            Title = apartment.Title;
            Rating = apartment.Rating;
            City = apartment.City;
            District = apartment.District;
            Street = apartment.Street;
            StreetNumber = apartment.StreetNumber;
            FloorNumber = apartment.FloorNumber;
            UserID = apartment.UserID;
        }

        public int ApartmentId { get; private set; }
        public string Title { get; private set; }
        public byte Type { get; private set; }
        public decimal Price { get; private set; }
        public short BedroomCount { get; private set; }
        public float Rating { get; private set; }

        public string City { get; private set; }
        public string District { get; private set; }
        public string Street { get; private set; }
        public short StreetNumber { get; private set; }
        public short FloorNumber { get; private set; }

        public int UserID { get; private set; }
    }
}