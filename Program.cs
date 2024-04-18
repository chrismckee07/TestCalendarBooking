using System.Globalization;
using TestCalendarBooking.Constants;
using TestCalendarBooking.Contexts;
using TestCalendarBooking.Models;

namespace TestCalendarBooking;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new BookingContext())
        {
            var allowedTimes = new string[] { "09:00",
                "09:30",
                "10:00",
                "10:30",
                "11:00",
                "11:30",
                "12:00",
                "12:30",
                "13:00",
                "13:30",
                "14:00",
                "14:30",
                "15:00",
                "15:30",
                "16:00",
                "16:30",
                "17:00"};

            Console.WriteLine(StringConstants.OptionsMessage);
            var response = Console.ReadLine();

            switch (response?.ToLower())
            {
                case "add":
                    AddBooking(context, allowedTimes);
                    break;
                case "delete":
                    DeleteBooking(context);
                    break;
                case "find":
                    FindAvailableBookings(context, allowedTimes);
                    break;
                case "keep":
                    KeepBooking(context, allowedTimes);
                    break;
                default:
                    Console.WriteLine("Invalid response was detected, operation cannot continue");
                    break;
            }
        }
    }

    /// <summary>
    /// Asks user to supply a date and time and attempts to create a booking based on that, if unsuccessful suitable error is returned
    /// </summary>
    /// <param name="context"></param>
    static void AddBooking(BookingContext context, string[] allowedTimes)
    {
        Console.WriteLine(StringConstants.AddMessage);
        var stringDateTime = Console.ReadLine();

        DateTime bookingDateTime;
        var validDate = DateTime.TryParseExact(stringDateTime, "dd-MM HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out bookingDateTime);  //year will default to current, assume that is what is expected based on requirements

        if (validDate)
        {
            Console.WriteLine(StringConstants.FirstNamePrompt);
            var firstName = Console.ReadLine();
            Console.WriteLine(StringConstants.LastNamePrompt);
            var lastName = Console.ReadLine();

            var bookingDate = bookingDateTime.ToString("yy-dd-MM"); //will save year to database so that booking slots can be used across years  
            var bookingTime = bookingDateTime.ToString("HH:mm");

            if (!allowedTimes.Contains(bookingTime))
            {
                Console.WriteLine(StringConstants.InvalidTime);
                return;
            }
            
            var keptTimes = context.Bookings.Where(x => x.Action == ActionConstants.Keep && x.BookingTime == bookingTime);
            var existingBooking = context.Bookings.Where(x => x.BookingDate.ToString() == bookingDate && x.BookingTime.ToString() == bookingTime &&
                (x.Action == ActionConstants.Added || x.Action == ActionConstants.Unavailable));

            if (keptTimes.Any() || existingBooking.Any())
            {
                Console.WriteLine("That time is not available, please try again, or try typing 'find' first to find a list of available times for a day");
                return;
            }

            CreateNewBooking(firstName, lastName, bookingDate, bookingTime, ActionConstants.Added, context);
        }
        else
            Console.WriteLine(StringConstants.InvalidDate);
    }

    /// <summary>
    /// Attempts to delete a booking that was created previously, assuming that it was created by the same user
    /// </summary>
    /// <param name="context"></param>
    static void DeleteBooking(BookingContext context)
    {
        Console.WriteLine(StringConstants.DeleteMessage);
        var stringDateTime = Console.ReadLine();
        DateTime bookingDateTime;

        var validDate = DateTime.TryParseExact(stringDateTime, "dd-MM HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out bookingDateTime);

        if (validDate)
        {
            Console.WriteLine(StringConstants.FirstNamePrompt);
            var firstName = Console.ReadLine();
            Console.WriteLine(StringConstants.LastNamePrompt);
            var lastName = Console.ReadLine();

            var bookingDate = bookingDateTime.ToString("yy-dd-MM"); //will save year to database so that booking slots can be used across years  
            var bookingTime = bookingDateTime.ToString("HH:mm");

            var keptTime = context.Bookings.Where(x => x.Action == ActionConstants.Keep && x.BookingDate == bookingDate).FirstOrDefault();
            var existingBooking = context.Bookings.Where(x => x.BookingDate.ToString() == bookingDate && x.BookingTime.ToString() == bookingTime &&
                (x.Action == ActionConstants.Added || x.Action == ActionConstants.Unavailable)).FirstOrDefault();

            if (keptTime != null)
            {
                if (keptTime.PersonFirstName != firstName || keptTime.PersonLastName != lastName) //don't allow user to delete someone elses booking
                {
                    Console.WriteLine(StringConstants.CannotDeleteExistingBooking);
                    return;
                }

                context.Bookings.Remove(keptTime);
                context.SaveChanges();
            }
            else if (existingBooking != null)
            {
                if (existingBooking.PersonFirstName != firstName || existingBooking.PersonLastName != lastName) //don't allow user to delete someone elses booking
                {
                    Console.WriteLine(StringConstants.CannotDeleteExistingBooking);
                    return;
                }
                context.Bookings.Remove(existingBooking);
                context.SaveChanges();
            }
            else
                Console.WriteLine("Booking not found or doesn't exist");
        }
        else
            Console.WriteLine(StringConstants.InvalidDate);

        Console.WriteLine("Booking successfully deleted.");
    }

    /// <summary>
    /// Returns a list of all available booking slots for a given day
    /// </summary>
    /// <param name="context"></param>
    static void FindAvailableBookings(BookingContext context, string[] allowedTimes)
    {
        Console.WriteLine(StringConstants.FindMessage);
        var stringDate = Console.ReadLine();

        DateOnly bookingDate;
        var validDate = DateOnly.TryParseExact(stringDate, "dd-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out bookingDate);

        if (validDate)
        {
            foreach (var allowedTime in allowedTimes)
            {
                var existingBooking = context.Bookings.Where(x => x.BookingDate == bookingDate.ToString("yy-dd-MM") && x.BookingTime == allowedTime 
                    && x.Action == ActionConstants.Added);

                var keptBookings = context.Bookings.Where(x => x.BookingTime == allowedTime && x.Action == ActionConstants.Keep);

                if (existingBooking.Any() || keptBookings.Any())
                    Console.WriteLine($"{allowedTime} - Taken");

                Console.WriteLine($"{allowedTime} - Available"); 
            }
        }
    }

    /// <summary>
    /// Attempts to keep a recurring timeslot on all days, will return error if existing booking exists on any day
    /// </summary>
    /// <param name="context"></param>
    static void KeepBooking(BookingContext context, string[] allowedTimes)
    {
        Console.WriteLine(StringConstants.KeepMessage);
        var bookingTime = Console.ReadLine();

        if (!allowedTimes.Contains(bookingTime))
        {
            Console.WriteLine(StringConstants.InvalidTime);
            return;
        }

        var keptTimes = context.Bookings.Where(x => x.Action == ActionConstants.Keep && x.BookingTime == bookingTime);
        var existingBooking = context.Bookings.Where(x => x.BookingTime.ToString() == bookingTime &&
            (x.Action == ActionConstants.Added || x.Action == ActionConstants.Unavailable));

        if (keptTimes.Any() || existingBooking.Any())
        {
            Console.WriteLine("That time is not available, please try again, or try typing 'find' first to find a list of available times");
            return;
        }

        Console.WriteLine(StringConstants.FirstNamePrompt);
        var firstName = Console.ReadLine();
        Console.WriteLine(StringConstants.LastNamePrompt);
        var lastName = Console.ReadLine();

        CreateNewBooking(firstName, lastName, null, bookingTime, ActionConstants.Keep, context);
    }

    private static void CreateNewBooking(string firstName, string lastName, string bookingDate, string bookingTime, string action, BookingContext context)
    {
        var newBooking = new Booking
        {
            PersonFirstName = firstName,
            PersonLastName = lastName,
            BookingDate = bookingDate,
            BookingTime = bookingTime,
            Action = action
        };

        context.Bookings.Add(newBooking);
        context.SaveChanges();

        Console.WriteLine("Booking successfully created");
    }
}