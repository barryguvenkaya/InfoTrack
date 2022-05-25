namespace InfoTrack.Services;

using InfoTrack.Abstract;
using InfoTrack.Extensions;
using InfoTrack.Models;

public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;

    public BookingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Booking>> GetBookingsAsync()
    {
        return await _unitOfWork.Bookings.GetBookings();
    }

    public async Task<Booking> GetBookingAsync(Guid bookingId)
    {
        return await _unitOfWork.Bookings.GetBooking(bookingId);
    }

    public async Task<int> AddBookingAsync(Booking request)
    {
        if (!SimultaneousBookingAllowed(request))
            throw new BadHttpRequestException($"AddBookingAsync fails for {request.BookingTime}: exceeds maximum simultaneous bookings", 409);

        _unitOfWork.Bookings.Add(request);
        return await _unitOfWork.CompleteAsync();
    }

    public async Task<Booking> FindBookingAsync(Guid id)
    {
        return await _unitOfWork.Bookings.FindAsync(id);
    }

    public async Task<int> RemoveBookingAsync(Booking item)
    {
        _unitOfWork.Bookings.Remove(item);
        return await _unitOfWork.CompleteAsync();
    }

    public bool SimultaneousBookingAllowed(Booking request)
    {
        var requestTime = request.BookingTime.ToTimeOnly();
        return _unitOfWork.Bookings.GetBookings().GetAwaiter().GetResult().Where( t => 
        (t.BookingTime.ToTimeOnly() <= requestTime && t.BookingTime.ToTimeOnly().AddHours(1) > requestTime) || 
        (t.BookingTime.ToTimeOnly() <= requestTime.AddHours(1) && t.BookingTime.ToTimeOnly().AddHours(1) > requestTime.AddHours(1))
        ).ToList().Count < 4;
    }

}