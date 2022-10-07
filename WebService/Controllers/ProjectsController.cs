using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;
using Project = WebService.Models.Generated.Project;

namespace WebService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly ProFindContext _context;

    public ProjectsController(ProFindContext context)
    {
        _context = context;
    }

    // GET: api/Projects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
    {
        if (_context.Projects == null)
        {
            return NotFound();
        }

        return await _context.Projects.Include(x => x.IdP1Navigation).Include(x => x.IdC1Navigation)
            .Include(x => x.TimeRequiredTr1Navigation).ToListAsync();
    }

    // projects of a professional
    [HttpGet("professional/{id}")]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjectsOfProfessional(string id)
    {
        if (_context.Projects == null)
        {
            return NotFound();
        }

        return await _context.Projects.Where(x => x.IdP1 == id).Include(x => x.IdP1Navigation).Include(x => x.IdC1Navigation)
            .Include(x => x.TimeRequiredTr1Navigation).Distinct().ToListAsync();
    }
    
    // projects of a client
    [HttpGet("client/{id}")]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjectsOfClient(string id)
    {
        if (_context.Projects == null)
        {
            return NotFound();
        }

        return await _context.Projects.Where(x => x.IdC1 == id).Include(x => x.IdP1Navigation).Include(x => x.IdC1Navigation)
            .Include(x => x.TimeRequiredTr1Navigation).Distinct().ToListAsync();
    }

    // GET: api/Projects/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetProject(string id)
    {
        if (_context.Projects == null)
        {
            return NotFound();
        }

        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        return project;
    }

    // PUT: api/Projects/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProject(string id, Project project)
    {
        if (id != project.IdPj)
        {
            return BadRequest();
        }

        _context.Entry(project).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProjectExists(id))
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
    
    // Mark a project as completed
    [HttpPut("complete/{id}")]
    public async Task<IActionResult> CompleteProject(string id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        project.Completed = true;
        _context.Entry(project).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProjectExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/Projects
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Project>> PostProject(Project project)
    {
        if (_context.Projects == null)
        {
            return Problem("Entity set 'ProFindContext.Projects'  is null.");
        }

        project.AssignId();
        _context.Projects.Add(project);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (ProjectExists(project.IdPj))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetProject", new { id = project.IdPj }, project);
    }

    // DELETE: api/Projects/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(string id)
    {
        if (_context.Projects == null)
        {
            return NotFound();
        }

        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("filter/")]
    public async Task<ActionResult<IEnumerable<Project>>> FilterProjects([FromQuery] string? projectStatusId,
        [FromQuery] string? TitlePJ)
    {
        var result = await (from proj in _context.Projects
            where (
                (TitlePJ == null ? true : proj.TitlePj.Contains(TitlePJ)))
            select proj).ToListAsync();

        if (result.Any() == false) return NotFound();
        else return result;
    }

    [HttpGet("search/")]
    public async Task<ActionResult<IEnumerable<Project>>> SearchProjects([FromQuery] string TitlePJ)
    {
        if (TitlePJ.Length == 0) return await GetProjects();
        else
        {
            var result = await (from proj in _context.Projects
                where (
                    proj.TitlePj.Contains(TitlePJ))
                select proj).ToListAsync();

            if (result.Any() == false) return NotFound();
            else return result;
        }
    }

    private bool ProjectExists(string id)
    {
        return (_context.Projects?.Any(e => e.IdPj == id)).GetValueOrDefault();
    }
}