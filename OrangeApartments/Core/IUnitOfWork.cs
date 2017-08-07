using System;
using OrangeApartments.Core.Repositories;

namespace OrangeApartments.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IApartmentRepository Apartments { get; }
        IChatRepository Chat { get; }
        IUserCommentsRepository UserComments { get; }
        IApartmentCommentsRepository ApartmentComments { get; }
        IApartmentBookingRepository ApartmentBooking { get; }
        IUserWatchListRepository UserWatchList { get; }
        ITagRepository Tags { get; }
        IApartmentTagRepository ApartmentTags { get; }

        int SaveChanges();
    }
}
