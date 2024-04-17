I have created a solution that addresses all of the requirements in the list of instructions.

Firstly the app is very rough around the edges, due to the fact that when developing I like to just get all of my
logic and work flow down first and then once that is in place go back and refactor the code bit by bit 
to make cleaner and compliant with the solid principles, whilst also beggining to work on writing unit 
tests that will ensure that the changes don't break scenarios that are already passing. 

How it works:
The user is first asked which action that they would like to perform; 'add', 'find', 'keep' or 'delete'.

For 'add', the user is asked to supply a date and time in the format of dd-MM hh:mm. In the background, yy 
of the current year will automatically be added to the date so that dates can be re-used in the following 
year. They are also asked to supply a first name and last name to record who made the booking.
A check is done to ensure that the date and time that were suplied are valid (eg. only 09:00 and 09:30 
are allowed, not 09:15), another check is done to ensure that a booking doesn't allready exist for that 
particular date and time, and if everything checks out, the booking is created.

For 'find', the user is asked to supply a date in the format of 'dd-MM', the database will then be queried 
to loop through the list of available timeslots, and will return a list to the user with booked slots flagged 
as 'Taken', and unbooked slots flagged as 'Available'.

For 'keep', the logic is very similar to 'add', with the exception that the user need only supply a time in the 
format of hh:mm. Like with 'add', a check is done to ensure that the time is valid, and another to ensure that 
the timeslot provided by the user is not taken on ANY day, since keep reserves that timeslot for everyday.

for 'delete', the user is required to supply datetime in dd-mm hh:mm format of the booking that they want to 
delete. They are then required to supply a first name and last name and match it with the record that they 
are trying to delete to ensure that they are not able to delete a booking that someone else created.

If I had more time to work on this app: 
- I would create a separate BookingService class to actually process bookings, and a BookingRepository 
  that would be responsible for read/write database calls, this separates the responsibility from 
  everything being done in the Program.cs file (SRP).
- Unit tests: where I stopped with this app is usually the point at which I would start developing unit
  tests, so that I could begin the refactoring of code and ensuring that scenarios that were passing 
  before are still passing.
- EF Database migrations: I wasn't able to generate the EF Migration scripts to create the database in
  time - I haven't had to set this up before so would need time to research how to implement it. 
  I have attached to SQL Scripts that can be run to create the database, table, and pre-poulate the 
  table with the blocked out dates for every 2nd day of every 3rd week in a month, which brings me to;
- The logic for not allowing bookings between 4pm-5pm on every 2nd day of every third week every month,
  I have created a script to pre-populate the database table with this info, but I am sure there is a 
  better way to do this, either in C# or SQL. Doing some research there are solutions 
  out there but I didn't have time to properly play around with it, if I had more time I definitely would
  because that would be preferable to clogging up the database with unecessary records
- There is still some instances of code duplication (I removed as many as I could without spending too much time), 
  that needs to be moved out into a separate method to help with code maintainability and readibility.
- Refactor refactor refactor heavily.
