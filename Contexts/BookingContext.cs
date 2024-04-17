using Microsoft.EntityFrameworkCore;
using TestCalendarBooking.Models;

namespace TestCalendarBooking.Contexts
{
    public class BookingContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Set your connection string here
            //optionsBuilder.UseSqlServer(@"Data Source=(TestCalendarBooking)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Appointments.mdf;Integrated Security=True"); //TODO: remove TrustServerCertificate=True
            //optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS01;Database=TestCalendarBooking;User Id=chris;Password=password;TrustServerCertificate=True"); 
            //optionsBuilder.UseSqlServer(@"Server=BETHANY\SQLEXPRESS;Database=TestCalendarBooking;User Id=chris;Password=password;TrustServerCertificate=True");
            optionsBuilder.UseSqlServer(@"Server=BETHANY\SQLEXPRESS;Database=TestCalendarBooking;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
