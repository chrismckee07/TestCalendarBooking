USE TestCalendarBooking
IF NOT EXISTS(SELECT NULL FROM sysobjects where name='Appointment' and xtype='U')
BEGIN
	CREATE TABLE Bookings 
	(
		Id INT PRIMARY KEY IDENTITY (1, 1),
		PersonFirstName VARCHAR(100) NULL,
		PersonLastName VARCHAR(100) NULL,
		BookingDate VARCHAR(20),
		BookingTime VARCHAR(20),
		Action VARCHAR(20) NULL
	)
END