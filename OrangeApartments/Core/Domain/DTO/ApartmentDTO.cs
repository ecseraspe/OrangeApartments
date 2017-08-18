using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeApartments.Core.Domain.DTO
{
    public class ApartmentDTO
    {
        public ApartmentDTO(Apartment a)
        {
            this.Price = a.Price;
            this.BedroomCount = a.BedroomCount;
            SleepingPlaces = a.SleepingPlaces;
            City = a.City;
        }
        public decimal Price { get; set; }
        public short BedroomCount { get; set; }
        public short SleepingPlaces { get; set; }
        [JsonIgnore]
        public string City { get; set; }

    }
}