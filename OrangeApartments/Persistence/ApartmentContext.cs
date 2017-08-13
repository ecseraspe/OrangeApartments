using System.Data.Entity;
using OrangeApartments.Core.Domain;

namespace OrangeApartments.Persistence
{
    public class ApartmentContext: DbContext
    {
        public ApartmentContext()
            : base("name=ApartmentContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Apartment> Apartments { get; set; }
        public virtual DbSet<ApartmentComments> ApartmentComments { get; set; }
        public virtual DbSet<UserComments> UserComments { get; set; }
        public virtual DbSet<Chat> Chat { get; set; }
        public virtual DbSet<ApartmentBooking> Booking { get; set; }
        public virtual DbSet<UserWatchList> UserWatchList { get; set; }
        public virtual DbSet<ApartmentTags> ApartmentTags { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}