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
            
        }
    }
}