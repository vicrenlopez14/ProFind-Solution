using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportticketsController : ControllerBase
    {
        private readonly ProFindContext _context;

        public SupportticketsController(ProFindContext context)
        {
            _context = context;
        }

        // GET: api/Supporttickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supportticket>>> GetSupporttickets()
        {
          if (_context.Supporttickets == null)
          {
              return NotFound();
          }
            return await _context.Supporttickets.ToListAsync();
        }

        // GET: api/Supporttickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supportticket>> GetSupportticket(string id)
        {
          if (_context.Supporttickets == null)
          {
              return NotFound();
          }
            var supportticket = await _context.Supporttickets.FindAsync(id);

            if (supportticket == null)
            {
                return NotFound();
            }

            return supportticket;
        }

        // PUT: api/Supporttickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupportticket(string id, Supportticket supportticket)
        {
            if (id != supportticket.IdSt)
            {
                return BadRequest();
            }

            _context.Entry(supportticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupportticketExists(id))
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

        // POST: api/Supporttickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Supportticket>> PostSupportticket(Supportticket supportticket)
        {
          if (_context.Supporttickets == null)
          {
              return Problem("Entity set 'ProFindContext.Supporttickets'  is null.");
          }
            _context.Supporttickets.Add(supportticket);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SupportticketExists(supportticket.IdSt))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSupportticket", new { id = supportticket.IdSt }, supportticket);
        }

        // DELETE: api/Supporttickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupportticket(string id)
        {
            if (_context.Supporttickets == null)
            {
                return NotFound();
            }
            var supportticket = await _context.Supporttickets.FindAsync(id);
            if (supportticket == null)
            {
                return NotFound();
            }

            _context.Supporttickets.Remove(supportticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupportticketExists(string id)
        {
            return (_context.Supporttickets?.Any(e => e.IdSt == id)).GetValueOrDefault();
        }
    }
}
