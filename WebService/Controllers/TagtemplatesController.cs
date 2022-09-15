using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagtemplatesController : ControllerBase
    {
        private readonly ProFindContext _context;

        public TagtemplatesController(ProFindContext context)
        {
            _context = context;
        }

        // GET: api/Tagtemplates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tagtemplate>>> GetTagtemplates()
        {
          if (_context.Tagtemplates == null)
          {
              return NotFound();
          }
            return await _context.Tagtemplates.ToListAsync();
        }

        // GET: api/Tagtemplates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tagtemplate>> GetTagtemplate(string id)
        {
          if (_context.Tagtemplates == null)
          {
              return NotFound();
          }
            var tagtemplate = await _context.Tagtemplates.FindAsync(id);

            if (tagtemplate == null)
            {
                return NotFound();
            }

            return tagtemplate;
        }

        // PUT: api/Tagtemplates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTagtemplate(string id, Tagtemplate tagtemplate)
        {
            if (id != tagtemplate.IdTt)
            {
                return BadRequest();
            }

            _context.Entry(tagtemplate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagtemplateExists(id))
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

        // POST: api/Tagtemplates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tagtemplate>> PostTagtemplate(Tagtemplate tagtemplate)
        {
          if (_context.Tagtemplates == null)
          {
              return Problem("Entity set 'ProFindContext.Tagtemplates'  is null.");
          }
            _context.Tagtemplates.Add(tagtemplate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TagtemplateExists(tagtemplate.IdTt))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTagtemplate", new { id = tagtemplate.IdTt }, tagtemplate);
        }

        // DELETE: api/Tagtemplates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTagtemplate(string id)
        {
            if (_context.Tagtemplates == null)
            {
                return NotFound();
            }
            var tagtemplate = await _context.Tagtemplates.FindAsync(id);
            if (tagtemplate == null)
            {
                return NotFound();
            }

            _context.Tagtemplates.Remove(tagtemplate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagtemplateExists(string id)
        {
            return (_context.Tagtemplates?.Any(e => e.IdTt == id)).GetValueOrDefault();
        }
    }
}
