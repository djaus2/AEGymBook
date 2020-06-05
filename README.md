Athletics Essendon Gym Bookings app 
------------------------------------

A sample project showcasing a blazor app using ef core with identity based upon
*Blazor with Identity* project.  
Started with the ToDo Blazor sample, by changing the colors to Red and Black! Essendon's Colors.

<https://github.com/stavroskasidis/BlazorWithIdentity>

Uses date time picker from
<https://github.com/nheath99/NodaTimePicker/tree/master/src/NodaTimePicker.Demo/Pages>

### How to run

Can view app on azurewebsites here: [Demo](https://athsess.azurewebsites.net)

1.  Install
    [dotnet-sdk-3.1.300](https://dotnet.microsoft.com/download/dotnet-core/3.1)
    and the latest [Visual Studio 2019](https://visualstudio.microsoft.com/vs/).

2.  Clone or download.

3.  Open the solution in Visual Studio and press F5.

4.  Create a user using the `Create Account` button in the login page or login
    if you have already created a user.

### Also Live demo for BlazorWithIdenity:

https://blazorwithidentity.azurewebsites.net/

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

### 2Dos
- Fix booking bugs for non Today bookings.
- Add Admin functions to Admin page (See list on that page).
- Map allowed booking times.
- Enable roles: athlete,coach,has pass,admin
- Table layout issue for buttons, only: Currently uses full screen.
