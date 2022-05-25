namespace InfoTrack.Models;

using Microsoft.EntityFrameworkCore;
public class ApplicationContext : DbContext
{
    public DbSet<Booking> Bookings => Set<Booking>();
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
}