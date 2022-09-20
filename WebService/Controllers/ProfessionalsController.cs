using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;
using Professional = WebService.Models.Generated.Professional;

namespace WebService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfessionalsController : ControllerBase
{
    private readonly ProFindContext _context;

    public ProfessionalsController(ProFindContext context)
    {
        _context = context;
    }

    [HttpPost("Register")]
    // POST: api/Professionals/Register
    public async Task<IActionResult> RegisterProfessional(Professional professional)
    {
        if (ModelState.IsValid)
        {
            _context.Add(professional);
            await _context.SaveChangesAsync();
            return Ok(professional);
        }

        return BadRequest(ModelState);
    }

    [HttpPost("Login")]
    // POST: api/Professionals/Login
    public async Task<IActionResult> LoginProfessional(Professional professional)
    {
        if (ModelState.IsValid)
        {
            var professionalFromDb =
                await _context.Professionals.FirstOrDefaultAsync(a =>
                    a.EmailP == professional.EmailP && a.PasswordP == professional.PasswordP);
            if (professionalFromDb != null)
            {
                return Ok(professionalFromDb);
            }
        }

        return BadRequest(ModelState);
    }


    // GET: api/Professionals
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Professional>>> GetProfessionals()
    {
        if (_context.Professionals == null)
        {
            return NotFound();
        }

        return await _context.Professionals.Include(comments => comments.Activitycomments)
            .Include(supportTickets => supportTickets.Supporttickets).ToListAsync();
    }

    // GET: api/Professionals/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Professional>> GetProfessional(string id)
    {
        if (_context.Professionals == null)
        {
            return NotFound();
        }

        var professional = await _context.Professionals.FindAsync(id);

        if (professional == null)
        {
            return NotFound();
        }

        return professional;
    }

    // PUT: api/Professionals/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProfessional(string id, Professional professional)
    {
        if (id != professional.IdP)
        {
            return BadRequest();
        }

        _context.Entry(professional).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProfessionalExists(id))
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

    // POST: api/Professionals
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Professional>> PostProfessional(Professional professional)
    {
        if (_context.Professionals == null)
        {
            return Problem("Entity set 'ProFindContext.Professionals'  is null.");
        }

        professional.AssignId();
        _context.Professionals.Add(professional);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (ProfessionalExists(professional.IdP))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetProfessional", new { id = professional.IdP }, professional);
    }

    [HttpGet("filter/")]
    public async Task<ActionResult<IEnumerable<Professional>>> FilterProfessionals([FromQuery] string? professionId,
        [FromQuery] string? departmentId,
        [FromQuery] string? name)
    {
        var result = await (from prof in _context.Professionals
                            where ( (professionId==null ? true : prof.IdDp1.ToString() == departmentId) && 
                            (name==null ? true :  prof.NameP.Contains(name)) 
                            && (departmentId == null ? true : prof.IdPfs1.ToString() == departmentId))
                            select prof).ToListAsync();

        if (result.Any() == false)
        {
            return NotFound();
        }

        return result;
    }


    // DELETE: api/Professionals/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfessional(string id)
    {
        if (_context.Professionals == null)
        {
            return NotFound();
        }

        var professional = await _context.Professionals.FindAsync(id);
        if (professional == null)
        {
            return NotFound();
        }

        _context.Professionals.Remove(professional);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProfessionalExists(string id)
    {
        return (_context.Professionals?.Any(e => e.IdP == id)).GetValueOrDefault();
    }
}