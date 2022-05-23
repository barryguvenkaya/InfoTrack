using InfoTrack.Extensions;
using System.ComponentModel.DataAnnotations;

namespace InfoTrack.Models
{
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
                throw new BadHttpRequestException($"{nameof(name)} cannot be null", 400);

            if (!ValidateBusinessHours(bookingTime))
                throw new BadHttpRequestException("Booking time must be within business hours", 400);

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
}
