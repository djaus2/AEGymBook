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

    public class BookingSlotsClient
    {
        private readonly HttpClient client;
        private const string ServiceEndpoint = "/api/BookingSlots";

        public BookingSlotsClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<BookingSlot>> GetSlotList()// (int Id = 0)
        {
            int Id = 0;
            var bookings = new BookingSlot[0];

            try
            {
                bookings = await client.GetFromJsonAsync<BookingSlot[]>(
                    ServiceEndpoint);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.InnerException);
            }
            if (Id > 0)
            {
                //var book = from b in bookings where b.AthleteId == Id select b;
                bookings = bookings.ToArray<BookingSlot>();
            }
            var books2 = bookings.OrderByDescending(b => b.Date).ThenBy(b => b.Time);
            return books2.ToList<BookingSlot>();
        }

        public async Task<List<BookingSlot>> GetSlotListFwd()// (int Id = 0)
        {
            var bookings = new BookingSlot[0];

            try
            {
                bookings = await client.GetFromJsonAsync<BookingSlot[]>(
                    ServiceEndpoint);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.InnerException);
            }

            var books = from b in bookings where b.Date.Date >= DateTime.Now.Date select b;
            bookings = books.ToArray<BookingSlot>();
            var books2 = bookings.OrderByDescending(b => b.Date).ThenBy(b => b.Time);
            var slots =  books2.ToList<BookingSlot>();

            return slots;
        }


        public async Task<BookingSlot> GetBookingSlot(DateTime date, TimeSpan time)
        {
            var bookings = new BookingSlot[0];

            try
            {

                bookings = await client.GetFromJsonAsync<BookingSlot[]>(
                    ServiceEndpoint);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.InnerException);
                return null;
            }

            var book = from b in bookings where (b.Date == date) && (b.Time == time) select b;
            return book.FirstOrDefault();
        }

        public async Task<List<BookingSlot>> GetBookingSlotsForDay(DateTime date)
        {
            List<TimeSpan> myBook = new List<TimeSpan>();
            var bookingsQ = new List<BookingSlot>();
            try
            {
                var books1 = await GetSlotList();
                var books = from b in books1 where (b._Date == date.ToString("yyyy-MM-dd")) select b;
                //bookingsQ = books.ToList<BookingSlot>();
                //int[] times = books.AsEnumerable().Select(s => s._Time).ToArray<int>();
                //foreach (var time in times)
                //{
                //    var spots = from t in books where t._Time == time select t;
                //    int count = spots.Count();
                //    if (count != 0)
                //    {
                //        TimeSpan timeTS = new TimeSpan(0, time * 30, 0);
                //        if (!myBook.Contains(timeTS))
                //            myBook.Add(timeTS);
                //    }
                //}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.InnerException);
            }

            return bookingsQ;
        }

        public async Task AddBookingSlot(BookingSlot bookingSlot)
        {
            bool exists = await Exists(bookingSlot);
            if (exists)
                return;
            await client.PostAsJsonAsync(ServiceEndpoint, bookingSlot);
        }

        public async Task DeleteBookingSlot(int id)
        {
            await client.DeleteAsync($"{ServiceEndpoint}/{id}");
        }


        /// /////// Checks ///////////////////////////

        public async Task<bool> Exists(BookingSlot bookingSlot)
        {
            return await Exists(bookingSlot.Date, bookingSlot.Time);
        }

        public async Task<bool> Exists(DateTime date, TimeSpan time)
        {
            int id = await GetBookingSlotId(date,time);
            return (id > 0);
        }

        public async Task<int> GetBookingSlotId(DateTime date, TimeSpan time)
        {
            var bookings = new BookingSlot[0];

            try
            {

                bookings = await client.GetFromJsonAsync<BookingSlot[]>(
                    ServiceEndpoint);
                var book = from b in bookings where (b.Date == date) && (b.Time == time) select b;
                BookingSlot slot = book.FirstOrDefault();
                if (slot == null)
                    return 0;
                return slot.Id;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.InnerException);
                return -1;
            }
        }

        public async Task<BookingSlot> GetBookingSlot(int id)
        {
            BookingSlot bookingSlot = await client.GetFromJsonAsync<BookingSlot>($"{ServiceEndpoint}/{id}");
            return bookingSlot;
        }
    }
}
