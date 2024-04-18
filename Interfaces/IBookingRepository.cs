using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCalendarBooking.Contexts;

namespace TestCalendarBooking.Interfaces
{
    public interface IBookingRepository
    {
        /// <summary>
        /// Asks user to supply a date and time and attempts to create a booking based on that, if unsuccessful suitable error is returned
        /// </summary>
        /// <param name="context"></param>
        void AddBooking(BookingContext context, string[] allowedTimes);

        /// <summary>
        /// Attempts to delete a booking that was created previously, assuming that it was created by the same user
        /// </summary>
        /// <param name="context"></param>
        void DeleteBooking(BookingContext context);

        /// <summary>
        /// Returns a list of all available booking slots for a given day
        /// </summary>
        /// <param name="context"></param>
        void FindAvailableBookings(BookingContext context, string[] allowedTimes);

        /// <summary>
        /// Attempts to keep a recurring timeslot on all days, will return error if existing booking exists on any day
        /// </summary>
        /// <param name="context"></param>
        void KeepBooking(BookingContext context, string[] allowedTimes);
    }
}
