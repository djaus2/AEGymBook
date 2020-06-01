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
                    "api/Bookings/getbookings");
            }
            catch
            {
              
            }

            return bookings;
        }

        public async Task<UserInfo0[]> GetUsers()
        {
            var users = new UserInfo0[0];

            try
            {
                users = await client.GetFromJsonAsync<UserInfo0[]>(
                    "api/Bookings/getusers");
            }
            catch
            {

            }

            return users;
        }

        public async Task<UserInfo0> GetUser(string name)
        {
            UserInfo0 user = new UserInfo0();

            try
            {
                user = await client.GetFromJsonAsync<UserInfo0>(
                    "api/Bookings/getUser?name="+name);
            }
            catch
            {

            }

            return user;
        }
    }
}
