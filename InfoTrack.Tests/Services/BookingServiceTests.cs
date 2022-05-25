namespace InfoTrack.Tests;

using Xunit;
using Moq;
using InfoTrack.Abstract;
using InfoTrack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfoTrack.Services;

public class BookingServiceTests
{
    public readonly Mock<IUnitOfWork> _dbServiceMock = new();
    
    [Fact]
    public async Task GetBookingsAsync_CanGetBookings()
    {
        var bookings = new List<Booking> { new Booking("Baz", "09:00"), new Booking("Bar", "12:00"), new Booking("Foo", "16:00") };
        _dbServiceMock.Setup(x => x.Bookings.GetBookings()).Returns(Task.FromResult(bookings));
        
        // Arrange
        BookingService bookingService = new(_dbServiceMock.Object);

        // Act
        var result = await bookingService.GetBookingsAsync();

        // Assert
        Assert.True(result.Count == 3);
        _dbServiceMock.Verify(x => x.Bookings.GetBookings(), Times.Once); // method was called exactly once with Moq
    }

    [Fact]
    public async Task AddBookingAsync_CanAddBooking()
    {
        var bookings = new List<Booking> { new Booking("Baz", "09:00"), new Booking("Bar", "12:00"), new Booking("Foo", "16:00") };
        _dbServiceMock.Setup(x => x.Bookings.GetBookings()).Returns(Task.FromResult(bookings));

        var newBooking = new Booking("Oop", "10:30");
        _dbServiceMock.Setup(x => x.Bookings.Add(newBooking));

        _dbServiceMock.Setup(x => x.CompleteAsync()).Returns(Task.FromResult(1));

        // Arrange
        BookingService bookingService = new(_dbServiceMock.Object);

        // Act
        var result = await bookingService.AddBookingAsync(newBooking);

        // Assert
        Assert.True(result == 1);
        _dbServiceMock.Verify(x => x.Bookings.Add(newBooking), Times.Once); // method was called exactly once with Moq
    }

    [Fact]
    public void SimultaneousBookingAllowed_ShouldReturnFalse()
    {
        var bookings = new List<Booking> { new Booking("Baz", "09:00"), new Booking("Bar", "09:15"), new Booking("Foo", "09:24"), new Booking("Foo", "09:29") };
        _dbServiceMock.Setup(x => x.Bookings.GetBookings()).Returns(Task.FromResult(bookings));
        
        var newBooking = new Booking("Lea", "09:30");

        // Arrange
        BookingService bookingService = new(_dbServiceMock.Object);

        // Act
        var result = bookingService.SimultaneousBookingAllowed(newBooking);

        // Assert
        Assert.False(result);
        _dbServiceMock.Verify(x => x.Bookings.GetBookings(), Times.Once); // method was called exactly once with Moq
    }

    [Fact]
    public void SimultaneousBookingAllowed_ShouldReturnTrue()
    {
        var bookings = new List<Booking> { new Booking("Baz", "09:00"), new Booking("Bar", "09:15"), new Booking("Foo", "09:24") };
        _dbServiceMock.Setup(x => x.Bookings.GetBookings()).Returns(Task.FromResult(bookings));

        var newBooking = new Booking("Lea", "09:30");

        // Arrange
        BookingService bookingService = new(_dbServiceMock.Object);

        // Act
        var result = bookingService.SimultaneousBookingAllowed(newBooking);

        // Assert
        Assert.True(result);
        _dbServiceMock.Verify(x => x.Bookings.GetBookings(), Times.Once); // method was called exactly once with Moq
    }
}
