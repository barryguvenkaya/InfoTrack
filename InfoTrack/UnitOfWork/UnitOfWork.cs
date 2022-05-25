namespace InfoTrack.UnitOfWork;

using InfoTrack.Abstract;
using InfoTrack.Models;
using InfoTrack.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _context;
    public UnitOfWork(ApplicationContext context)
    {
        _context = context;
        Bookings = new BookingRepository(_context);
    }
    public IBookingRepository Bookings { get; private set; }
    public int Complete()
    {
        return _context.SaveChanges();
    }

    public Task<int> CompleteAsync()
    {
        return _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}
