using TestCalendarBooking.Contexts;

namespace TestCalendarBooking.BookingRepository
{
    public class BookingRepositoryy
    {
        //Note: No logic in this class as this is just to demonstrate how dependancy injection would be implemented if I could work out a way to call methods from the static Main method
        //Context in injected here and can be used with Entity Frameork to read/write updates to database:
        private readonly BookingContext _bookingContext;

        public BookingRepositoryy(BookingContext bookingContext)
        {
            _bookingContext = bookingContext ?? throw new ArgumentNullException(nameof(bookingContext));
        }

        public void AddBooking(BookingContext context, string[] allowedTimes)
        { 
            //logic here to add booking 
        }

        public void DeleteBooking(BookingContext context)
        {
            //logic here to delete booking 
        }

        public void FindAvailableBookings(BookingContext context, string[] allowedTimes)
        {
            //logic here to find available booking times
        }

        public void KeepBooking(BookingContext context, string[] allowedTimes)
        {
            //logic here to keep booking time across all days
        }


        private void CreateNewBooking(string firstName, string lastName, string bookingDate, string bookingTime, string action, BookingContext context)
        {
            //logic to create new booking record
        }
    }
}
