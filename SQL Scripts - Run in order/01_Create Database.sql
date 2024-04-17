IF NOT EXISTS(SELECT NULL FROM sys.databases WHERE name = 'TestCalendarBooking')
BEGIN
	CREATE DATABASE TestCalendarBooking
END