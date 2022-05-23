using InfoTrack.Abstract;
using InfoTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace InfoTrack.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationContext context) : base(context)
        {
        }
        public async Task<List<Booking>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking> GetBooking(Guid bookingId)
        {
            return await _context.Bookings.FindAsync(bookingId);
        }

        public async Task<Booking> FindAsync(Guid bookingId)
        {
            return await _context.Bookings.FindAsync(bookingId);
        }
    }
}
