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
    #region BookingSlotsController
    [Route("api/[controller]")]
    [ApiController]
    public class BookingSlotsController : ControllerBase
    {
        private BookingsDBContext _context;

        public BookingSlotsController() //(BookingsDBContext context)
        {
            DbContextOptions<ApplicationDbContext> optionsBuilder = new DbContextOptions<ApplicationDbContext> ();
            //optionsBuilder.UseSqlite("Data Source=data.db");
            _context = new BookingsDBContext();
            //_context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            System.Diagnostics.Debug.WriteLine("==BookingSlotsController==");
        }
        #endregion

        // GET: api/BookingSlots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingSlot>>> GetBookingSlots()
        {
            return await _context.BookingSlots.ToListAsync();
        }

        #region snippet_GetByID
        // GET: api/BookingSlots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingSlot>> GetBookingSlots(int id)
        {
            var BookingSlot = await _context.BookingSlots.FindAsync(id);

            if (BookingSlot == null)
            {
                return NotFound();
            }

            return BookingSlot;
        }
        #endregion

        #region snippet_Update
        // PUT: api/BookingSlots/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingSlots(int id, BookingSlot BookingSlot)
        {
            if (id != BookingSlot.Id)
            {
                return BadRequest();
            }

            _context.Entry(BookingSlot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingSlotExists(id))
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

        #region snippet_Create
        // POST: api/BookingSlots
        [HttpPost]
        public async Task<ActionResult<BookingSlot>> PostBookingSlot(BookingSlot BookingSlot)
        {
            int i = 0;
            try
            {
                _context.BookingSlots.Add(BookingSlot);
                await _context.SaveChangesAsync();
             }
            catch (Microsoft.Data.Sqlite.SqliteException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine(i);
                System.Diagnostics.Debug.WriteLine(sqlEx.Message);
                System.Diagnostics.Debug.WriteLine(sqlEx.InnerException);
            }
            System.Diagnostics.Debug.WriteLine("Got to  Return");
            //return CreatedAtAction("GetBookingInfo", new { id = BookingSlot.Id }, BookingSlot);
            return CreatedAtAction(nameof(GetBookingSlots), new { id = BookingSlot.Id }, BookingSlot);

        }
        #endregion

        #region snippet_Delete
        // DELETE: api/BookingSlots/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookingSlot>> DeleteBookingSlot(int id)
        {
            bool exists = _context.BookingSlots.Any(e => e.Id == id);

            var BookingSlot = await _context.BookingSlots.FindAsync(id);
            if (BookingSlot == null)
            {
                return NotFound();
            }

            _context.BookingSlots.Remove(BookingSlot);
            await _context.SaveChangesAsync();

            return BookingSlot;
        }
        #endregion

        private bool BookingSlotExists(int id)
        {
            return _context.BookingSlots.Any(e => e.Id == id);
        }
    }
}

