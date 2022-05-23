namespace InfoTrack.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IBookingRepository Bookings { get; }
        int Complete();
        Task<int> CompleteAsync();
    }
}
