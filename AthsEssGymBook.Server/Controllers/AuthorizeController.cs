using AthsEssGymBook.Server.Data;
using AthsEssGymBook.Server.Models;
using AthsEssGymBook.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AthsEssGymBook.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthorizeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginParameters parameters)
        {
            var user = await _userManager.FindByNameAsync(parameters.UserName);
            if (user == null) return BadRequest("User does not exist");
            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, parameters.Password, false);
            if (!singInResult.Succeeded) return BadRequest("Invalid password");

            await _signInManager.SignInAsync(user, parameters.RememberMe);

            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterParameters parameters)
        {
            var user = new ApplicationUser();
            user.UserName = parameters.UserName;
            var result = await _userManager.CreateAsync(user, parameters.Password);
            if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);
            
            var usr = await _userManager.FindByNameAsync(user.UserName);
            usr.Email = parameters.Email;
            usr.PhoneNumber = parameters.Phone;
            await _userManager.UpdateAsync(usr);
            System.Diagnostics.Debug.WriteLine("====Doing Athlete mirror of user====");
            Athlete athlete;
            using (BookingsDBContext db = new BookingsDBContext())
            {
                athlete = new Athlete
                {
                    Id = 0,
                    UserName = user.UserName,
                    Email = user.Email,
                    HasAccessCard = user.HasAccessCard,
                    IsAdmin = user.IsAdmin,
                    IsCoach = user.IsCoach,
                    PhoneNumber = user.PhoneNumber
                };
                System.Diagnostics.Debug.WriteLine("====Done Athlete mirror of user====");
                try
                {
                    System.Diagnostics.Debug.WriteLine("====Doing Athlete mirror db - 2====");
                    await db.Athletes.AddAsync(athlete);
                    System.Diagnostics.Debug.WriteLine("====Doing Athlete mirror db - 3====");
                    await db.SaveChangesAsync();
                    System.Diagnostics.Debug.WriteLine("====Doing Athlete mirror db - 1r====");
                    var athletes = db.Athletes.ToList();
                    athlete = athletes[0];
                    System.Diagnostics.Debug.WriteLine(athlete.Id);
                }
                catch (Microsoft.Data.Sqlite.SqliteException sqlEx)
                {
                    System.Diagnostics.Debug.WriteLine("====Doing Athlete mirror db Error");
                    System.Diagnostics.Debug.WriteLine(sqlEx.Message);
                    System.Diagnostics.Debug.WriteLine(sqlEx.InnerException);
                }
            }
            System.Diagnostics.Debug.WriteLine(athlete.Id);
            using (BookingsDBContext db2 = new BookingsDBContext())
            {
                athlete.Id = 1;
                System.Diagnostics.Debug.WriteLine("====Doing Athlete mirror of user====");
                var booking = new BookingInfo
                {
                    Id = 0,
                    Date = new DateTime(2020, 2, 15),
                    _Time = 12,
                    _Duration = 2,
                    Slot = 1,
                    UserId = athlete.Id
                };
                System.Diagnostics.Debug.WriteLine("====Done Athlete mirror of user====");
                try
                {
                   // db2.Attach<BookingInfo>(booking);
                    System.Diagnostics.Debug.WriteLine("====Doing Booking mirror db - 2====");
                    await db2.BookingInfo.AddAsync(booking);
                    System.Diagnostics.Debug.WriteLine("====Doing Booking mirror db - 3====");
                    await db2.SaveChangesAsync();
                    System.Diagnostics.Debug.WriteLine("====Doing Booking mirror db - 4r====");
                }
                catch (Microsoft.Data.Sqlite.SqliteException sqlEx)
                {
                    System.Diagnostics.Debug.WriteLine("====Doing Athlete mirror db Error");
                    System.Diagnostics.Debug.WriteLine(sqlEx.Message);
                    System.Diagnostics.Debug.WriteLine(sqlEx.InnerException);
                }
                catch (Exception Ex)
                {
                    System.Diagnostics.Debug.WriteLine("====Doing Athlete mirror db Error");
                    System.Diagnostics.Debug.WriteLine(Ex.Message);
                    System.Diagnostics.Debug.WriteLine(Ex.InnerException);
                }
            }



            return await Login(new LoginParameters
            {
                UserName = parameters.UserName,
                Password = parameters.Password
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        public UserInfo UserInfo()
        {
            //var user = await _userManager.GetUserAsync(HttpContext.User);
            return BuildUserInfo();
        }


        private UserInfo BuildUserInfo()
        {
            return new UserInfo
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                UserName = User.Identity.Name,
                ExposedClaims = User.Claims
                    //Optionally: filter the claims you want to expose to the client
                    //.Where(c => c.Type == "test-claim")
                    .ToDictionary(c => c.Type, c => c.Value)
            };
        }
    }
}
