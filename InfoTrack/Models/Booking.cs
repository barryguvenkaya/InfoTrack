namespace InfoTrack.Models;

using InfoTrack.Extensions;
using InfoTrack.Helpers;
using System.ComponentModel.DataAnnotations;

public class Booking
{
    public Guid BookingId { get; private set; }
    [Required]
    public string BookingTime { get; private set; }
    [Required]
    public string Name { get; private set; }

    protected Booking() { } // this constructor is only for EF proxy creation

    public Booking(string name, string bookingTime)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new AppException($"Booking object creation failed: {nameof(name)} cannot be null!");

        if (!ValidateBusinessHours(bookingTime))
            throw new AppException($"Booking object creation failed: BookingTime must be within business hours!");

        BookingId = Guid.NewGuid();
        Name = name;
        BookingTime = bookingTime.ToTimeOnly().ToString("HH:mm");
    }

    private static bool ValidateBusinessHours(string bookingTime)
    {
        if (bookingTime.ToTimeOnly() > new TimeOnly(16, 0) || bookingTime.ToTimeOnly() < new TimeOnly(9, 0))
            return false;
        return true; 
    }
}
