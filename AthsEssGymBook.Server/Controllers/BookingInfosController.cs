using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AthsEssGymBook.Shared;
using AthsEssGymBook.Server.Data;
using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using AthsEssGymBook.Client.Pages;

namespace AthsEssGymBook.Server.Controllers
{
    #region BookingInfosController
    [Route("api/[controller]")]
    [ApiController]
    public class BookingInfosController : ControllerBase
    {
        private BookingsDBContext _context;

        public BookingInfosController() //(BookingsDBContext context)
        {
            DbContextOptions<ApplicationDbContext> optionsBuilder = new DbContextOptions<ApplicationDbContext> ();
            //optionsBuilder.UseSqlite("Data Source=data.db");
            _context = new BookingsDBContext();
            System.Diagnostics.Debug.WriteLine("==BookingInfosController==");
        }
        #endregion

        // GET: api/BookingInfos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingInfo>>> GetBookingInfos()
        {
            return await _context.BookingInfo.ToListAsync();
        }

        #region snippet_GetByID
        // GET: api/BookingInfos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingInfo>> GetBookingInfos(int id)
        {
            var BookingInfo = await _context.BookingInfo.FindAsync(id);

            if (BookingInfo == null)
            {
                return NotFound();
            }

            return BookingInfo;
        }
        #endregion

        #region snippet_Update
        // PUT: api/BookingInfos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingInfos(int id, BookingInfo BookingInfo)
        {
            if (id != BookingInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(BookingInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        #endregion

 
        public Athlete getAthlete(string name)
        {
            Athlete Athlete;
            using (var db = new BookingsDBContext())
            {
                var vAthlete = from u in db.Athletes where (u.UserName == name) select u;
                Athlete = vAthlete.FirstOrDefault<Athlete>();
            }
            return Athlete;
        }

        #region snippet_Create
        // POST: api/BookingInfos
        [HttpPost]
        public async Task<ActionResult<BookingInfo>> PostBookingInfos(BookingInfo BookingInfo)
        {
            int i = 0;
            try
            {
                _context.BookingInfo.Add(BookingInfo);
                await _context.SaveChangesAsync();
             }
            catch (Microsoft.Data.Sqlite.SqliteException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine(i);
                System.Diagnostics.Debug.WriteLine(sqlEx.Message);
                System.Diagnostics.Debug.WriteLine(sqlEx.InnerException);
            }
            System.Diagnostics.Debug.WriteLine("Got to  Return");
            //return CreatedAtAction("GetBookingInfo", new { id = BookingInfo.Id }, BookingInfo);
            return CreatedAtAction(nameof(GetBookingInfos), new { id = BookingInfo.Id }, BookingInfo);

        }
        #endregion

        #region snippet_Delete
        // DELETE: api/BookingInfos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookingInfo>> DeleteBookingInfos(int id)
        {
            bool exists = _context.BookingInfo.Any(e => e.Id == id);

            var BookingInfo = await _context.BookingInfo.FindAsync(id);
            if (BookingInfo == null)
            {
                return NotFound();
            }

            _context.BookingInfo.Remove(BookingInfo);
            await _context.SaveChangesAsync();

            return BookingInfo;
        }
        #endregion

        private bool BookingInfoExists(int id)
        {
            return _context.BookingInfo.Any(e => e.Id == id);
        }
    }
}

