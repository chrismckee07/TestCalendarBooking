namespace TestCalendarBooking.Constants
{
    public static class StringConstants
    {
        public const string OptionsMessage = "Enter 'find' to find a free booking for a day, 'add' to add a new booking for a day, 'delete' to remove a booking for a day, or 'keep' to keep a timeslot for a day:";
        public const string AddMessage = "Please enter the booking time in the format of DD-MM hh:mm that you would like to book - All apointments are 30mins in length:";
        public const string DeleteMessage = "Please enter the booking time in DD/MM hh:mm that you would like to delete:";
        public const string FindMessage = "Please enter the date in DD/MM to find available bookings:";
        public const string KeepMessage = "Please enter the booking time in hh:mm that you would like to book on an ongoing basis:";
        public const string InvalidResponse = $"Invalid response detected. {OptionsMessage}";
        public const string InvalidTime = "Time that was supplied was invalid, only 30 minute slots between 09:00 and 17:00 are available for selection. Please try again.";
        public const string InvalidDate = "Date was not in correct format, booking could not be created";
        public const string FirstNamePrompt = "Please enter first name for the booking:";
        public const string LastNamePrompt = "Please enter last name for the booking:";
        public const string CannotDeleteExistingBooking = "Existing booking was not made in your name, it cannot be deleted";
    }

    public static class ActionConstants
    {
        public const string Added = "Added";
        public const string Keep = "Keep";
        public const string Unavailable = "Unavailable";
    }
}
