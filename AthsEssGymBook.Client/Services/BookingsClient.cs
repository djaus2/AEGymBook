using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthsEssGymBook.Client.Services
{
    using AthsEssGymBook.Client.Pages;
    using AthsEssGymBook.Shared;
    using System.Dynamic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class BookingsClient
    {
        private readonly HttpClient client;
        private const string ServiceEndpoint = "/api/BookingInfos";

        public BookingsClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<BookingInfo>> GetBookings()
        {
            var bookings = new BookingInfo[0];

            try
            {
                bookings = await client.GetFromJsonAsync<BookingInfo[]>(
                    ServiceEndpoint);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.InnerException);
            }

            return bookings.ToList<BookingInfo>();
        }

        public async Task<List<BookingInfo>> GetBookings(DateTime date)
        {
            var bookings = new BookingInfo[0];
            var bookingsQ = new List<BookingInfo>();
            try
            {
                bookings = await client.GetFromJsonAsync<BookingInfo[]>(
                    ServiceEndpoint);
                var books = from b in bookings where (b._Date == date.ToString("yyyy-mm-dd")) select b;
                bookingsQ.AddRange(books);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.InnerException);
            }

            return bookingsQ;
        }

        public async Task<List<BookingInfo>> GetBookingsForAthlete(Athlete athlete)
        {
            var bookings = new BookingInfo[0];
            var bookingsQ = new List<BookingInfo>();
            try
            {
                bookings = await client.GetFromJsonAsync<BookingInfo[]>(
                    ServiceEndpoint);
                var books = from b in bookings where (b.AthleteId == athlete.Id) select b;
                bookingsQ.AddRange(books);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.InnerException);
            }

            return bookingsQ;
        }

        public async Task<BookingInfo> GetBooking(int id)
        {
            BookingInfo booking = await client.GetFromJsonAsync<BookingInfo>($"{ServiceEndpoint}/{id}");
            return booking;
        }

        public async Task AddBooking(BookingInfo booking)
        {
            await client.PostAsJsonAsync(ServiceEndpoint, booking);
        }
    }
}
