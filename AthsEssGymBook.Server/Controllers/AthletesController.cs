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

namespace AthsEssGymBook.Server.Controllers
{
    #region AthletesController
    [Route("api/[controller]")]
    [ApiController]
    public class AthletesController : ControllerBase
    {
        private BookingsDBContext _context;

        public AthletesController() //(BookingsDBContext context)
        {
            DbContextOptions<ApplicationDbContext> optionsBuilder = new DbContextOptions<ApplicationDbContext> ();
            //optionsBuilder.UseSqlite("Data Source=data.db");
            _context = new BookingsDBContext();
            System.Diagnostics.Debug.WriteLine("==AthletesController==");
        }
        #endregion

        // GET: api/Athletes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Athlete>>> GetAthletes()
        {
            System.Diagnostics.Debug.WriteLine("==GetAthletes==");
            return await _context.Athletes.ToListAsync();
        }

        #region snippet_GetByID
        //// GET: api/Athletes/5
        //[HttpGet("{id}")]
        //public Athlete GetAthletes(long id)
        //{
        //    //var Athlete = await _context.Athletes.FindAsync(id);

        //    //if (Athlete == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //return Athlete;

        //    var vAthlete = from u in _context.Athletes where (u.Id== id) select u;
        //    //if (Athlete == null)
        //    //{
        //    //    return NotFound();
        //    //}
        //    Athlete athlete = vAthlete.FirstOrDefault<Athlete>();
        //    return athlete;
        //    //return await (Task<ActionResult<Athlete>>)vAthlete;
        //}
        #endregion

        #region snippet_GetByID
        // GET: api/Athletes/5
        [HttpGet("{name}")]
        public Athlete GetAthletes(string name)
        {

            var vAthlete = from u in _context.Athletes where (u.UserName == name) select u;
            //if (Athlete == null)
            //{
            //    return NotFound();
            //}
            Athlete athlete = vAthlete.FirstOrDefault<Athlete>();
            return athlete;
        }

        #endregion

        #region snippet_Update
        // PUT: api/Athletes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAthletes(long id, Athlete Athlete)
        {
            if (id != Athlete.Id)
            {
                return BadRequest();
            }

            _context.Entry(Athlete).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AthleteExists(id))
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
        // POST: api/Athletes
        [HttpPost]
        public async Task<ActionResult<Athlete>> PostAthletes(Athlete Athlete)
        {
            int i = 0;
            try
            {

                //i++;
                //Athlete = new Athlete { AthleteId = usr.Id, Slot = 1, Date = new DateTime(2020, 6, 1), Time = new TimeSpan(14, 0, 0), _Duration = 2 };
                i++;
                _context.Athletes.Add(Athlete);
                i++;
                await _context.SaveChangesAsync();
                i++;
             }
            catch (Microsoft.Data.Sqlite.SqliteException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine(i);
                System.Diagnostics.Debug.WriteLine(sqlEx.Message);
                System.Diagnostics.Debug.WriteLine(sqlEx.InnerException);
            }
            System.Diagnostics.Debug.WriteLine("Got to  Return");
            //return CreatedAtAction("GetAthlete", new { id = Athlete.Id }, Athlete);
            return CreatedAtAction(nameof(GetAthletes), new { id = Athlete.Id }, Athlete);

        }
        #endregion

        #region snippet_Delete
        // DELETE: api/Athletes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Athlete>> DeleteAthletes(long id)
        {
            var Athlete = await _context.Athletes.FindAsync(id);
            if (Athlete == null)
            {
                return NotFound();
            }

            _context.Athletes.Remove(Athlete);
            await _context.SaveChangesAsync();

            return Athlete;
        }
        #endregion

        private bool AthleteExists(long id)
        {
            return _context.Athletes.Any(e => e.Id == id);
        }
    }
}

