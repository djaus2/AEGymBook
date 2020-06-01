using AthsEssGymBook.Server.Data;
using AthsEssGymBook.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthsEssGymBook.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class BookingsController : Controller
    {

        [HttpGet("[action]")]
        public string GetUserX(string name)
        {
            UserInfo0 usr;
            using (var db = new BookingsDBContext())
            {
                var user = from u in db.AspNetUsers where (u.UserName == name) select u;
                usr = user.FirstOrDefault<UserInfo0>();
            }
            return usr.UserName;
        }

        [HttpGet("[action]")]
        public bool Count(string name)
        {
            UserInfo0 usr;
            using (var db = new BookingsDBContext())
            {
                var user = from u in db.AspNetUsers where (u.UserName == name) select u;
                usr = user.FirstOrDefault<UserInfo0>();
            }
            return usr.IsAdmin;
        }

        [HttpGet("[action]")]
        public UserInfo0  getUser(string name)
        {
            UserInfo0 user;
            using (var db = new BookingsDBContext())
            {
                var vUser = from u in db.AspNetUsers where (u.UserName == name) select u;
                user = vUser.FirstOrDefault<UserInfo0>();
            }
            return user;
        }

        [HttpGet("[action]")]
        public IEnumerable<BookingInfo> getbookings()
        {
            List<BookingInfo> bookings;
            using (var db = new BookingsDBContext())
            {
                var vbookings = from b in db.BookingInfo  select b;
                bookings = vbookings.ToList<BookingInfo>();
            }
            return bookings ;
        }
        

        [HttpGet("[action]")]
        public IEnumerable<UserInfo0> getusers()
        {
            List<UserInfo0> users;
            using (var db = new BookingsDBContext())
            {
                var vusers = from b in db.AspNetUsers select b;
                users = vusers.ToList<UserInfo0>();
            }
            return users;
        }


    }
}
