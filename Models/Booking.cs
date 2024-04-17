using System.ComponentModel.DataAnnotations;

namespace TestCalendarBooking.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public string PersonFirstName { get; set; } 
        public string PersonLastName { get; set; } 
        public string BookingDate { get; set; }     //Make strings for easier comparison to user inputs
        public string BookingTime { get; set; }     //Make strings for easier comparison to user inputs
        public string Action { get; set; }
    }
}
