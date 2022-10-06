using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimerequiredsController : ControllerBase
    {
        private readonly ProFindContext _context;

        public TimerequiredsController(ProFindContext context)
        {
            _context = context;
        }

        // GET: api/Timerequireds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Timerequired>>> GetTimerequireds()
        {
            return await _context.Timerequireds.ToListAsync();
        }

        // GET: api/Timerequireds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Timerequired>> GetTimerequired(int id)
        {
            var timerequired = await _context.Timerequireds.FindAsync(id);

            if (timerequired == null)
            {
                return NotFound();
            }

            return timerequired;
        }

        // PUT: api/Timerequireds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimerequired(int id, Timerequired timerequired)
        {
            if (id != timerequired.IdTr)
            {
                return BadRequest();
            }

            _context.Entry(timerequired).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimerequiredExists(id))
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

        // POST: api/Timerequireds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Timerequired>> PostTimerequired(Timerequired timerequired)
        {
            _context.Timerequireds.Add(timerequired);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTimerequired", new { id = timerequired.IdTr }, timerequired);
        }

        // DELETE: api/Timerequireds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimerequired(int id)
        {
            var timerequired = await _context.Timerequireds.FindAsync(id);
            if (timerequired == null)
            {
                return NotFound();
            }

            _context.Timerequireds.Remove(timerequired);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TimerequiredExists(int id)
        {
            return _context.Timerequireds.Any(e => e.IdTr == id);
        }
    }
}