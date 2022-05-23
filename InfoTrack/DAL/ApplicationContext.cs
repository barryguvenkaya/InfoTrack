using Microsoft.EntityFrameworkCore;

namespace InfoTrack.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Booking> Bookings => Set<Booking>();
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        //}

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Booking>().Property(p => p.BloggerName).HasMaxLength(10);
        //}
    }
}