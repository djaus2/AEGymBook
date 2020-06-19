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
using System.Security.Claims;
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

            if (!Settings.HaveSetupAdmin)
            {
                Settings.HaveSetupAdmin = true;
                AddAdminAccount(Settings.AdminName,Settings.AdminPwd).GetAwaiter();
            }
        }

        public async Task AddAdminAccount(string name,string pwd)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = name;
            user.Name = name;
            user.IsAdmin = true;
            user.Email = "";
            user.PhoneNumber = "";
            user.LockoutEnabled = false;
            ApplicationUser usr = await _userManager.FindByNameAsync(user.UserName);
            if (usr == null)
            {
                var result = await _userManager.CreateAsync(user, pwd);
                if (!result.Succeeded) return;

                ApplicationUser usr2 = await _userManager.FindByNameAsync(user.UserName);
                await AddUserToAthletes(usr2);
            }
        }

        private async Task AddUserToAthletes(ApplicationUser user)
        {
            Athlete athlete;
            using (BookingsDBContext db = new BookingsDBContext())
            {
                db.Database.EnsureCreated();
                athlete = new Athlete
                {
                    Id = 0,
                    UserName = user.UserName,
                    Name = user.Name,
                    Email = user.Email,
                    IsAdmin = user.IsAdmin,
                    IsCoach = user.IsCoach,
                    CanSetSlots = user.CanSetSlots,
                    HasAccessCard = user.HasAccessCard,
                    IsMember = user.IsMember,
                    PhoneNumber = user.PhoneNumber
                };
                try
                {
                    await db.Athletes.AddAsync(athlete);
                    await db.SaveChangesAsync();
                    //Should have some check here:
                    var athletes = db.Athletes.ToList();
                    athlete = athletes[0];
                    System.Diagnostics.Debug.WriteLine(athlete.Id);
                }
                catch (Microsoft.Data.Sqlite.SqliteException sqlEx)
                {
                    System.Diagnostics.Debug.WriteLine(sqlEx.Message);
                    System.Diagnostics.Debug.WriteLine(sqlEx.InnerException);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginParameters parameters)
        {
            var user = await _userManager.FindByNameAsync(parameters.UserName);
            if (user == null) return BadRequest("User does not exist");
            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, parameters.Password, false);
            if (!singInResult.Succeeded) return BadRequest("Invalid password");

            await _signInManager.SignInAsync(user, parameters.RememberMe);
            bool IsAdmin = user.IsAdmin;
            bool IsCoach = user.IsCoach;
            bool HasAccessCard = user.HasAccessCard;

            

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
            usr.Mobile = parameters.Mobile;
            usr.HasAccessCard = false;
            usr.CanSetSlots = false;
            usr.IsCoach = false;
            usr.IsAdmin = false;
            usr.IsMember = parameters.IsMember;
            usr.PhoneNumberConfirmed = true;
            usr.EmailConfirmed = true;
            await _userManager.UpdateAsync(usr);
            await AddUserToAthletes(usr);
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
