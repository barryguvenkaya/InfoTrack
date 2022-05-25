namespace InfoTrack.Abstract;

using InfoTrack.Models;

public interface IBookingService
{
    Task<List<Booking>> GetBookingsAsync();
    Task<Booking> GetBookingAsync(Guid bookingId);
    Task<int> AddBookingAsync(Booking request);
    Task<Booking> FindBookingAsync(Guid id);
    Task<int> RemoveBookingAsync(Booking item);
    bool SimultaneousBookingAllowed(Booking request);
}
