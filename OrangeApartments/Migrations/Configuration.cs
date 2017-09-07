namespace OrangeApartments.Migrations
{
    using OrangeApartments.Core.Domain;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<OrangeApartments.Persistence.ApartmentContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(OrangeApartments.Persistence.ApartmentContext context)
        {
            context.Users.AddOrUpdate(x => x.Mail, new User()
            {
                FirstName = "admin",
                LastName = "admin",
                IsAdmin = true,
                Mail = "admin@admin.adim",
                Password = "admin"
            });

            Tag[] tagsList = new Tag[] {
                            new Tag() { TagName = "Free WiFi" },
                             new Tag() { TagName = "With children" },
                             new Tag() { TagName = "No Pets" },
                             new Tag() { TagName = "Without children" },
                             new Tag() { TagName = "With Pets" },
                             new Tag() { TagName = "Internet" },
                             new Tag() { TagName = "Dryer" },
                             new Tag() { TagName = "Air conditioning" },
                             new Tag() { TagName = "Family amenities" },
                             new Tag() { TagName = "Private entrance" },
                             new Tag() { TagName = "Free parking" },
                             new Tag() { TagName = "Iron" },
                             new Tag() { TagName = "Laundy" } };
            context.Tag.AddOrUpdate(x => x.TagName, tagsList);
            context.SaveChanges();
        }
    }
}
