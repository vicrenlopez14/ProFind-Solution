using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityanswerprofessionalsController : ControllerBase
    {
        private readonly ProFindContext _context;

        public SecurityanswerprofessionalsController(ProFindContext context)
        {
            _context = context;
        }

        // GET: api/Securityanswerprofessionals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Securityanswerprofessional>>> GetSecurityanswerprofessionals()
        {
          if (_context.Securityanswerprofessionals == null)
          {
              return NotFound();
          }
            return await _context.Securityanswerprofessionals.ToListAsync();
        }

        // GET: api/Securityanswerprofessionals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Securityanswerprofessional>> GetSecurityanswerprofessional(string id)
        {
          if (_context.Securityanswerprofessionals == null)
          {
              return NotFound();
          }
            var securityanswerprofessional = await _context.Securityanswerprofessionals.FindAsync(id);

            if (securityanswerprofessional == null)
            {
                return NotFound();
            }

            return securityanswerprofessional;
        }

        // PUT: api/Securityanswerprofessionals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecurityanswerprofessional(string id, Securityanswerprofessional securityanswerprofessional)
        {
            if (id != securityanswerprofessional.IdSa)
            {
                return BadRequest();
            }

            _context.Entry(securityanswerprofessional).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecurityanswerprofessionalExists(id))
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

        // POST: api/Securityanswerprofessionals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Securityanswerprofessional>> PostSecurityanswerprofessional(Securityanswerprofessional securityanswerprofessional)
        {
          if (_context.Securityanswerprofessionals == null)
          {
              return Problem("Entity set 'ProFindContext.Securityanswerprofessionals'  is null.");
          }
            _context.Securityanswerprofessionals.Add(securityanswerprofessional);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SecurityanswerprofessionalExists(securityanswerprofessional.IdSa))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSecurityanswerprofessional", new { id = securityanswerprofessional.IdSa }, securityanswerprofessional);
        }

        // DELETE: api/Securityanswerprofessionals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecurityanswerprofessional(string id)
        {
            if (_context.Securityanswerprofessionals == null)
            {
                return NotFound();
            }
            var securityanswerprofessional = await _context.Securityanswerprofessionals.FindAsync(id);
            if (securityanswerprofessional == null)
            {
                return NotFound();
            }

            _context.Securityanswerprofessionals.Remove(securityanswerprofessional);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SecurityanswerprofessionalExists(string id)
        {
            return (_context.Securityanswerprofessionals?.Any(e => e.IdSa == id)).GetValueOrDefault();
        }
    }
}
