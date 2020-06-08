Athletics Essendon Gym Bookings app 
------------------------------------

Can view app on azurewebsites here: [Demo](https://athsess.azurewebsites.net)  

My Blog Posts on this _(3 thus far)_: [At http://www.sportronics.com.au](http://www.sportronics.com.au/search.html?query=gym+book)

## About
A sample project showcasing Blazor WebApps.  
An app using Enity Framework Core, C#, .NET Core etc.
Uses  with identity based upon the *Blazor with Identity* sample project:  
<https://github.com/stavroskasidis/BlazorWithIdentity>  
Also uses the date time picker from:  
<https://github.com/nheath99/NodaTimePicker/tree/master/src/NodaTimePicker.Demo/Pages>  

## App Requirements
The app is a Booking App to manage bookings for a club Gym.  
Given the CV-19 pandemic, the gym needs to limit the number (4) of participants at a time.
  - Logged in users can view all previous bookings, and filter date-time
  - Users can view their bookings, and filter date-time
  - Users find available time slots and book up to the limit at that time (i.e. 4)
    - Also un-book
    - Of course bookings only forward in date-time.
  
Live demo for blazorwithidentity [Demo](https://blazorwithidentity.azurewebsites.net)

### Status
- Uses SQLite
- Can register and login.
- Can add bookings for logged in user
- Can see all bookings with various filters.
- Can see bookings for logged in user
- Can install on phone. _(Android tested)_

### Done 2Dos
- Restrict bookings for any time slot to the app limit _**Done**_
- User can only book a timeslot once  _**Done**_   _(In dev mode can book more than once though)._
- Bookings Orderby Date decending then by Time of day ascending  _**Done**_
- Mechanism for user to delete their bookings. _**Done**_
- Set Min day to book as Today.  _**Done**_
- Add Admin page and restrict who it shows to. _**Done**
- App settings static class _**Done**_

### 2Dos
- Fix booking bugs for non Today bookings. _**Done**_
- Add Admin functions to Admin page (See list on that page).
- Map allowed booking times.
- Enable roles: athlete,coach,has pass,admin
- Table layout issue for buttons, only: Currently uses full screen. _**Largely one**_
- Put settings in DB and allow Admin to change online.
- Implement I forgot pwd. Further no email capability yet.
- Social Media Logins
