using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityanswerclientsController : ControllerBase
    {
        private readonly ProFindContext _context;

        public SecurityanswerclientsController(ProFindContext context)
        {
            _context = context;
        }

        // GET: api/Securityanswerclients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Securityanswerclient>>> GetSecurityanswerclients()
        {
          if (_context.Securityanswerclients == null)
          {
              return NotFound();
          }
            return await _context.Securityanswerclients.ToListAsync();
        }

        // GET: api/Securityanswerclients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Securityanswerclient>> GetSecurityanswerclient(string id)
        {
          if (_context.Securityanswerclients == null)
          {
              return NotFound();
          }
            var securityanswerclient = await _context.Securityanswerclients.FindAsync(id);

            if (securityanswerclient == null)
            {
                return NotFound();
            }

            return securityanswerclient;
        }

        // PUT: api/Securityanswerclients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecurityanswerclient(string id, Securityanswerclient securityanswerclient)
        {
            if (id != securityanswerclient.IdSa)
            {
                return BadRequest();
            }

            _context.Entry(securityanswerclient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecurityanswerclientExists(id))
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

        // POST: api/Securityanswerclients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Securityanswerclient>> PostSecurityanswerclient(Securityanswerclient securityanswerclient)
        {
          if (_context.Securityanswerclients == null)
          {
              return Problem("Entity set 'ProFindContext.Securityanswerclients'  is null.");
          }
            _context.Securityanswerclients.Add(securityanswerclient);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SecurityanswerclientExists(securityanswerclient.IdSa))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSecurityanswerclient", new { id = securityanswerclient.IdSa }, securityanswerclient);
        }

        // DELETE: api/Securityanswerclients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecurityanswerclient(string id)
        {
            if (_context.Securityanswerclients == null)
            {
                return NotFound();
            }
            var securityanswerclient = await _context.Securityanswerclients.FindAsync(id);
            if (securityanswerclient == null)
            {
                return NotFound();
            }

            _context.Securityanswerclients.Remove(securityanswerclient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SecurityanswerclientExists(string id)
        {
            return (_context.Securityanswerclients?.Any(e => e.IdSa == id)).GetValueOrDefault();
        }
    }
}
