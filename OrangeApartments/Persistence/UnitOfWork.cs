using OrangeApartments.Core;
using OrangeApartments.Core.Repositories;
using OrangeApartments.Persistence.Repository;

namespace OrangeApartments.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApartmentContext _context;

        public UnitOfWork(ApartmentContext context)
        {
            this._context = context;
            Users = new UserRepository(_context);
        }

        public IUserRepository Users { get; private set; }
        public IApartmentRepository Apartments { get; private set; }
        public IChatRepository Chat { get; private set; }
        public IUserCommentsRepository UserComments { get; private set; }
        public IApartmentCommentsRepository ApartmentComments { get; private set; }
        public IApartmentBookingRepository ApartmentBooking { get; private set; }
        public IUserWatchListRepository UserWatchList { get; private set; }
        public ITagRepository Tags { get; private set; }
        public IApartmentTagRepository ApartmentTags { get; private set; }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}