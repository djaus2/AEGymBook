﻿using System;
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

        public async Task<List<BookingInfo>> GetBookingList(int Id = 0)
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
            if (Id > 0)
            {
                var book = from b in bookings where b.AthleteId == Id select b;
                bookings = book.ToArray<BookingInfo>();
            }
            var books2 = bookings.OrderByDescending(b => b.Date).ThenBy(b => b.Time);
            return books2.ToList<BookingInfo>();
        }

        public async Task<BookingInfo[]> GetBookings(int Id = 0)
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
            if (Id >0)
            {
                var book = from b in bookings where b.AthleteId == Id select b;
                bookings = book.ToArray<BookingInfo>();
            }
            var books2 = bookings.OrderByDescending(b => b.Date).ThenBy(b => b.Time);
            return bookings;
        }

        public async Task<List<BookingInfo>> GetBookings(DateTime date,int Id = 0)
        {
            var bookings = new BookingInfo[0];
            var bookingsQ = new List<BookingInfo>();
            try
            {
                bookings = await GetBookings( Id);
                var books = from b in bookings where (b._Date == date.ToString("yyyy-MM-dd")) select b;
                bookingsQ.AddRange(books);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.InnerException);
            }

            return bookingsQ;
        }

        public async Task <List<TimeSpan>> GetMyBookingsForDay(DateTime date, int Id)
        {
             List<TimeSpan> myBook = new List<TimeSpan>();
            var bookings = new BookingInfo[0];
            var bookingsQ = new List<BookingInfo>();
            try
            {
                bookings = await GetBookings(Id);
                var books = from b in bookings where (b._Date == date.ToString("yyyy-MM-dd")) select b;
                int[] times = books.AsEnumerable().Select(s => s._Time).ToArray<int>();
                foreach (var time in times)
                {
                    var spots = from t in books where t._Time == time select t;
                    int count = spots.Count();
                    if (count != 0)
                    {
                        TimeSpan timeTS = new TimeSpan(0, time * 30, 0);
                        if (! myBook.Contains(timeTS))
                            myBook.Add(timeTS);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.InnerException);
            }

            return myBook;
        }

        public async Task<Dictionary<TimeSpan, int>> GetBookingCountForDay(DateTime date)
        {
            Dictionary<TimeSpan, int> dict = new Dictionary<TimeSpan, int>();
            var bookings = new BookingInfo[0];
            var bookingsQ = new List<BookingInfo>();
            try
            {
                bookings = await GetBookings();
                var books = from b in bookings where (b._Date == date.ToString("yyyy-MM-dd")) select b;
                int[] times = books.AsEnumerable().Select(s => s._Time).ToArray<int>();
                foreach (var time in times)
                {
                    var spots = from t in books where t._Time == time select t;
                    int count = spots.Count();
                    if (count != 0)
                    {
                        dict[new TimeSpan(0, time * 30, 0)] = count;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.InnerException);
            }

            return dict;
        }

        public async Task<List<BookingInfo>> GetBookingsFrom(DateTime date, int Id = 0)
        {
            var bookings = new BookingInfo[0];
            var bookingsQ = new List<BookingInfo>();
            try
            {
                bookings = await GetBookings( Id);
                System.Diagnostics.Debug.WriteLine(date);
                System.Diagnostics.Debug.WriteLine(DateTime.Compare(new DateTime(2020,6,20), date));
                // b._Date GE date is >=0
                var books = from b in bookings where DateTime.Compare( b.Date , date) >=0  select b;
                bookingsQ.AddRange(books);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.InnerException);
            }

            return bookingsQ;
        }

        public async Task<List<BookingInfo>> GetBookingsSelectedWeek(DateTime date, int Id=0)
        {
            var bookings = new BookingInfo[0];
            var bookingsQ = new List<BookingInfo>();
            int dayOfTheWeek = (int)date.DayOfWeek;
            DateTime startDate = date.Subtract(new TimeSpan(dayOfTheWeek, 0, 0, 0, 0));
            DateTime endDate = startDate.Add(new TimeSpan(8, 0, 0, 0, 0));
            System.Diagnostics.Debug.WriteLine(startDate);
            System.Diagnostics.Debug.WriteLine(date);
            System.Diagnostics.Debug.WriteLine(endDate);
            try
            {
                bookings = await GetBookings(Id);
                var books = from b in bookings where (DateTime.Compare(b.Date, startDate)>0) && (DateTime.Compare(b.Date, endDate)<0) select b;
                bookingsQ.AddRange(books);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.InnerException);
            }

            return bookingsQ;
        }

        public async Task<List<BookingInfo>> GetBookingsForAthlete(int Id)
        {
            var bookings = new BookingInfo[0];
            var bookingsQ = new List<BookingInfo>();
            try
            {
                bookings = await client.GetFromJsonAsync<BookingInfo[]>(
                    ServiceEndpoint);
                var books = from b in bookings where (b.AthleteId == Id) select b;
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

        public async Task DeleteBooking(int id)
        {
            await client.DeleteAsync($"{ServiceEndpoint}/{id}");
        }
    }
}
