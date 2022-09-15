using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjecttemplatesController : ControllerBase
    {
        private readonly ProFindContext _context;

        public ProjecttemplatesController(ProFindContext context)
        {
            _context = context;
        }

        // GET: api/Projecttemplates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projecttemplate>>> GetProjecttemplates()
        {
          if (_context.Projecttemplates == null)
          {
              return NotFound();
          }
            return await _context.Projecttemplates.ToListAsync();
        }

        // GET: api/Projecttemplates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Projecttemplate>> GetProjecttemplate(string id)
        {
          if (_context.Projecttemplates == null)
          {
              return NotFound();
          }
            var projecttemplate = await _context.Projecttemplates.FindAsync(id);

            if (projecttemplate == null)
            {
                return NotFound();
            }

            return projecttemplate;
        }

        // PUT: api/Projecttemplates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjecttemplate(string id, Projecttemplate projecttemplate)
        {
            if (id != projecttemplate.IdPt)
            {
                return BadRequest();
            }

            _context.Entry(projecttemplate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjecttemplateExists(id))
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

        // POST: api/Projecttemplates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Projecttemplate>> PostProjecttemplate(Projecttemplate projecttemplate)
        {
          if (_context.Projecttemplates == null)
          {
              return Problem("Entity set 'ProFindContext.Projecttemplates'  is null.");
          }
            _context.Projecttemplates.Add(projecttemplate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProjecttemplateExists(projecttemplate.IdPt))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProjecttemplate", new { id = projecttemplate.IdPt }, projecttemplate);
        }

        // DELETE: api/Projecttemplates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjecttemplate(string id)
        {
            if (_context.Projecttemplates == null)
            {
                return NotFound();
            }
            var projecttemplate = await _context.Projecttemplates.FindAsync(id);
            if (projecttemplate == null)
            {
                return NotFound();
            }

            _context.Projecttemplates.Remove(projecttemplate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjecttemplateExists(string id)
        {
            return (_context.Projecttemplates?.Any(e => e.IdPt == id)).GetValueOrDefault();
        }
    }
}
