using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectpaysController : ControllerBase
    {
        private readonly ProFindContext _context;

        public ProjectpaysController(ProFindContext context)
        {
            _context = context;
        }

        // GET: api/Projectpays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projectpay>>> GetProjectpays()
        {
          if (_context.Projectpays == null)
          {
              return NotFound();
          }
            return await _context.Projectpays.ToListAsync();
        }

        // GET: api/Projectpays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Projectpay>> GetProjectpay(string id)
        {
          if (_context.Projectpays == null)
          {
              return NotFound();
          }
            var projectpay = await _context.Projectpays.FindAsync(id);

            if (projectpay == null)
            {
                return NotFound();
            }

            return projectpay;
        }

        // PUT: api/Projectpays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectpay(string id, Projectpay projectpay)
        {
            if (id != projectpay.IdPpy)
            {
                return BadRequest();
            }

            _context.Entry(projectpay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectpayExists(id))
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

        // POST: api/Projectpays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Projectpay>> PostProjectpay(Projectpay projectpay)
        {
          if (_context.Projectpays == null)
          {
              return Problem("Entity set 'ProFindContext.Projectpays'  is null.");
          }
            _context.Projectpays.Add(projectpay);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProjectpayExists(projectpay.IdPpy))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProjectpay", new { id = projectpay.IdPpy }, projectpay);
        }

        // DELETE: api/Projectpays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectpay(string id)
        {
            if (_context.Projectpays == null)
            {
                return NotFound();
            }
            var projectpay = await _context.Projectpays.FindAsync(id);
            if (projectpay == null)
            {
                return NotFound();
            }

            _context.Projectpays.Remove(projectpay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectpayExists(string id)
        {
            return (_context.Projectpays?.Any(e => e.IdPpy == id)).GetValueOrDefault();
        }
    }
}
