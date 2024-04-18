using Microsoft.EntityFrameworkCore;
using TestCalendarBooking.Models;

namespace TestCalendarBooking.Contexts
{
    public class BookingContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Set your connection string here:
            optionsBuilder.UseSqlServer(@"Server=BETHANY\SQLEXPRESS;Database=TestCalendarBooking;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
