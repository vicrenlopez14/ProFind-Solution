using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;
using Proposal = WebService.Models.Generated.Proposal;

namespace WebService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProposalsController : ControllerBase
{
    private readonly ProFindContext _context;

    public ProposalsController(ProFindContext context)
    {
        _context = context;
    }

    // GET: api/Proposals
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Proposal>>> GetProposals()
    {
        if (_context.Proposals == null)
        {
            return NotFound();
        }

        return await _context.Proposals.Include(x => x.IdC3Navigation).Include(x => x.IdP3Navigation).ToListAsync();
    }

    // GET: api/Proposals/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Proposal>> GetProposal(string id)
    {
        if (_context.Proposals == null)
        {
            return NotFound();
        }

        var proposal = await _context.Proposals.FindAsync(id);

        if (proposal == null)
        {
            return NotFound();
        }

        return proposal;
    }

    [HttpGet("search/")]
    public async Task<ActionResult<IEnumerable<Proposal>>> SearchProposals([FromQuery] string proposalsId,
        [FromQuery] string titleProposals)
    {
        var result = await (from proposal in _context.Proposals
            where (proposal.IdPp == proposalsId && proposal.TitlePp.Contains(titleProposals))
            select proposal).ToListAsync();

        if (result.Any() == false)
        {
            return NotFound();
        }

        return result;
    }

    [HttpGet("search/paginated")]
    public async Task<ActionResult<IEnumerable<Proposal>>> SearchProposalsPaginated([FromQuery] string proposalsId,
        [FromQuery] string titleProposals, [FromQuery] string limit, [FromQuery] string offset)
    {
        var query = (from proposal in _context.Proposals
            where (proposal.IdPp == proposalsId && proposal.TitlePp.Contains(titleProposals))
            select proposal);

        query = (from proposals in _context.Proposals select proposals).OrderByDescending(x => x.SuggestedStart)
            .Skip(int.Parse(offset)).Take(int.Parse(limit));

        var result = await query.ToListAsync();

        if (result.Any() == false)
        {
            return NotFound();
        }

        return result;
    }

    // Get proposals from a client
    [HttpGet("client/{id}")]
    public async Task<ActionResult<IEnumerable<Proposal>>> GetProposalsFromClient(string id)
    {
        var result = await (from proposal in _context.Proposals
            where (proposal.IdC3 == id)
            select proposal).Distinct().ToListAsync();

        if (result.Any() == false)
        {
            return NotFound();
        }

        return result;
    }

    [HttpGet("filter/")]
    public async Task<ActionResult<IEnumerable<Proposal>>> FilterProposals([FromQuery] DateTime? suggestedStart,
        [FromQuery] string? suggestedStarRel,
        [FromQuery] DateTime? suggestedEnd, [FromQuery] string? suggestedEndRel,
        [FromQuery] string? idProfessional,
        [FromQuery] string? idClient)
    {
        var query = _context.Proposals.Where(proposal => true);

        if (idProfessional != null)
        {
            query = _context.Proposals.Where(proposal => proposal.IdP3 == idProfessional);
        }

        if (idClient != null)
        {
            query = _context.Proposals.Where(proposal => proposal.IdC3 == idClient);
        }

        if (suggestedStart != null)
        {
            switch (suggestedStarRel)
            {
                case "lte":
                    query = _context.Proposals.Where(proposal => proposal.SuggestedStart <= suggestedStart);
                    break;
                case "eq":
                    query = _context.Proposals.Where(proposal => proposal.SuggestedStart == suggestedStart);
                    break;
                case "gte":
                    query = _context.Proposals.Where(proposal => proposal.SuggestedStart >= suggestedStart);
                    break;
            }
        }

        if (suggestedEnd != null)
        {
            switch (suggestedEndRel)
            {
                case "lte":
                    query = _context.Proposals.Where(proposal => proposal.SuggestedEnd <= suggestedEnd);
                    break;
                case "eq":
                    query = _context.Proposals.Where(proposal => proposal.SuggestedEnd == suggestedEnd);
                    break;
                case "gte":
                    query = _context.Proposals.Where(proposal => proposal.SuggestedEnd >= suggestedEnd);
                    break;
            }
        }

        var result = await query.ToListAsync();

        if (result.Any() == false)
        {
            return NotFound();
        }

        return result;
    }

    [HttpGet("paginated")]
    public async Task<ActionResult<IEnumerable<Proposal>>> GetProposalsPaginated([FromQuery] string limit,
        [FromQuery] string offset)
    {
        var query = (from proposals in _context.Proposals select proposals).OrderByDescending(x => x.SuggestedStart)
            .Skip(int.Parse(offset)).Take(int.Parse(limit));

        return await _context.Proposals.ToListAsync();
    }

    // PUT: api/Proposals/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProposal(string id, Proposal proposal)
    {
        if (id != proposal.IdPp)
        {
            return BadRequest();
        }


        _context.Entry(proposal).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProposalExists(id))
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

    // POST: api/Proposals
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Proposal>> PostProposal(Proposal proposal)
    {
        if (_context.Proposals == null)
        {
            return Problem("Entity set 'ProFindContext.Proposals'  is null.");
        }

        proposal.AssignId();
        _context.Proposals.Add(proposal);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (ProposalExists(proposal.IdPp))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetProposal", new { id = proposal.IdPp }, proposal);
    }

    // DELETE: api/Proposals/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProposal(string id)
    {
        if (_context.Proposals == null)
        {
            return NotFound();
        }

        var proposal = await _context.Proposals.FindAsync(id);
        if (proposal == null)
        {
            return NotFound();
        }

        _context.Proposals.Remove(proposal);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProposalExists(string id)
    {
        return (_context.Proposals?.Any(e => e.IdPp == id)).GetValueOrDefault();
    }
}