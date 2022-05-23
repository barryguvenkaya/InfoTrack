using InfoTrack.Models;

namespace InfoTrack.Abstract
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<List<Booking>> GetBookings();
        Task<Booking> GetBooking(Guid bookingId);
        Task<Booking> FindAsync(Guid bookingId);
    }
}
