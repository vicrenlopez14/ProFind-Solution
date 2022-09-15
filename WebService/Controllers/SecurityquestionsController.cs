using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityquestionsController : ControllerBase
    {
        private readonly ProFindContext _context;

        public SecurityquestionsController(ProFindContext context)
        {
            _context = context;
        }

        // GET: api/Securityquestions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Securityquestion>>> GetSecurityquestions()
        {
          if (_context.Securityquestions == null)
          {
              return NotFound();
          }
            return await _context.Securityquestions.ToListAsync();
        }

        // GET: api/Securityquestions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Securityquestion>> GetSecurityquestion(string id)
        {
          if (_context.Securityquestions == null)
          {
              return NotFound();
          }
            var securityquestion = await _context.Securityquestions.FindAsync(id);

            if (securityquestion == null)
            {
                return NotFound();
            }

            return securityquestion;
        }

        // PUT: api/Securityquestions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecurityquestion(string id, Securityquestion securityquestion)
        {
            if (id != securityquestion.IdSq)
            {
                return BadRequest();
            }

            _context.Entry(securityquestion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecurityquestionExists(id))
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

        // POST: api/Securityquestions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Securityquestion>> PostSecurityquestion(Securityquestion securityquestion)
        {
          if (_context.Securityquestions == null)
          {
              return Problem("Entity set 'ProFindContext.Securityquestions'  is null.");
          }
            _context.Securityquestions.Add(securityquestion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SecurityquestionExists(securityquestion.IdSq))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSecurityquestion", new { id = securityquestion.IdSq }, securityquestion);
        }

        // DELETE: api/Securityquestions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecurityquestion(string id)
        {
            if (_context.Securityquestions == null)
            {
                return NotFound();
            }
            var securityquestion = await _context.Securityquestions.FindAsync(id);
            if (securityquestion == null)
            {
                return NotFound();
            }

            _context.Securityquestions.Remove(securityquestion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SecurityquestionExists(string id)
        {
            return (_context.Securityquestions?.Any(e => e.IdSq == id)).GetValueOrDefault();
        }
    }
}
