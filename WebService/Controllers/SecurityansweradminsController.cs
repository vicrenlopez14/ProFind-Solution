using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityansweradminsController : ControllerBase
    {
        private readonly ProFindContext _context;

        public SecurityansweradminsController(ProFindContext context)
        {
            _context = context;
        }

        // GET: api/Securityansweradmins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Securityansweradmin>>> GetSecurityansweradmins()
        {
          if (_context.Securityansweradmins == null)
          {
              return NotFound();
          }
            return await _context.Securityansweradmins.ToListAsync();
        }

        // GET: api/Securityansweradmins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Securityansweradmin>> GetSecurityansweradmin(string id)
        {
          if (_context.Securityansweradmins == null)
          {
              return NotFound();
          }
            var securityansweradmin = await _context.Securityansweradmins.FindAsync(id);

            if (securityansweradmin == null)
            {
                return NotFound();
            }

            return securityansweradmin;
        }

        // PUT: api/Securityansweradmins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecurityansweradmin(string id, Securityansweradmin securityansweradmin)
        {
            if (id != securityansweradmin.IdSa)
            {
                return BadRequest();
            }

            _context.Entry(securityansweradmin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecurityansweradminExists(id))
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

        // POST: api/Securityansweradmins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Securityansweradmin>> PostSecurityansweradmin(Securityansweradmin securityansweradmin)
        {
          if (_context.Securityansweradmins == null)
          {
              return Problem("Entity set 'ProFindContext.Securityansweradmins'  is null.");
          }
            _context.Securityansweradmins.Add(securityansweradmin);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SecurityansweradminExists(securityansweradmin.IdSa))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSecurityansweradmin", new { id = securityansweradmin.IdSa }, securityansweradmin);
        }

        // DELETE: api/Securityansweradmins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecurityansweradmin(string id)
        {
            if (_context.Securityansweradmins == null)
            {
                return NotFound();
            }
            var securityansweradmin = await _context.Securityansweradmins.FindAsync(id);
            if (securityansweradmin == null)
            {
                return NotFound();
            }

            _context.Securityansweradmins.Remove(securityansweradmin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SecurityansweradminExists(string id)
        {
            return (_context.Securityansweradmins?.Any(e => e.IdSa == id)).GetValueOrDefault();
        }
    }
}
