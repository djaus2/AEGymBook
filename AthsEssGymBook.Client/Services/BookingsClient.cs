using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthsEssGymBook.Client.Services
{
    using AthsEssGymBook.Shared;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class BookingsClient
    {
        private readonly HttpClient client;

        public BookingsClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<BookingInfo[]> GetBookings()
        {
            var bookings = new BookingInfo[0];

            try
            {
                bookings = await client.GetFromJsonAsync<BookingInfo[]>(
                    "api/Bookings/GetBookingInfos");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.InnerException);
            }

            return bookings;
        }

        public async Task<Athlete[]> GetAthletes()
        {
            var Athletes = new Athlete[0];

            try
            {
                Athletes = await client.GetFromJsonAsync<Athlete[]>(
                    "api/Bookings2/getAthletes");
            }
            catch
            {

            }

            return Athletes;
        }

        public async Task<Athlete> GetAthlete(string name)
        {
            Athlete Athlete = new Athlete();

            try
            {
                Athlete = await client.GetFromJsonAsync<Athlete>(
                    "api/Bookings2/getAthlete?name="+name);
            }
            catch
            {

            }

            return Athlete;
        }

        public async Task AddBooking(BookingInfo booking)
        {
            await client.PostAsJsonAsync("api/Bookings/AddBooking", booking);
        }
    }
}
