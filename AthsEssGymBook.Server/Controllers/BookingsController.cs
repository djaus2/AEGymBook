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
        public IEnumerable<UserInfo0> GetUserX(string name)
        {
            List<UserInfo0> usr;
            using (var db = new BookingsDBContext())
            {
                var user = from u in db.AspNetUsers where (u.UserName == name) select u;
                usr = user.ToList<UserInfo0>();
            }
            return usr;
        }

        [HttpGet("[action]")]
        public int Count(string name)
        {
            using (var db = new BookingsDBContext())
            {
                var user = from u in db.AspNetUsers where (u.UserName == name) select u;
                var us = user.FirstOrDefault<UserInfo0>();
            }
            return 137;
        }


    }
}
