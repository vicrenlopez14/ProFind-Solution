using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceandconditionsproposalsController : ControllerBase
    {
        private readonly ProFindContext _context;

        public PriceandconditionsproposalsController(ProFindContext context)
        {
            _context = context;
        }

        // GET: api/Priceandconditionsproposals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Priceandconditionsproposal>>> GetPriceandconditionsproposals()
        {
            return await _context.Priceandconditionsproposals.Include(x => x.IdPp1Navigation).ToListAsync();
        }

        // GET: api/Priceandconditionsproposals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Priceandconditionsproposal>> GetPriceandconditionsproposal(string id)
        {
            var priceandconditionsproposal = await _context.Priceandconditionsproposals.FindAsync(id);

            if (priceandconditionsproposal == null)
            {
                return NotFound();
            }

            return priceandconditionsproposal;
        }

        // PUT: api/Priceandconditionsproposals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriceandconditionsproposal(string id, Priceandconditionsproposal priceandconditionsproposal)
        {
            if (id != priceandconditionsproposal.IdPcp)
            {
                return BadRequest();
            }

            _context.Entry(priceandconditionsproposal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceandconditionsproposalExists(id))
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

        // POST: api/Priceandconditionsproposals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Priceandconditionsproposal>> PostPriceandconditionsproposal(Priceandconditionsproposal priceandconditionsproposal)
        {
            _context.Priceandconditionsproposals.Add(priceandconditionsproposal);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PriceandconditionsproposalExists(priceandconditionsproposal.IdPcp))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPriceandconditionsproposal", new { id = priceandconditionsproposal.IdPcp }, priceandconditionsproposal);
        }

        // DELETE: api/Priceandconditionsproposals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriceandconditionsproposal(string id)
        {
            var priceandconditionsproposal = await _context.Priceandconditionsproposals.FindAsync(id);
            if (priceandconditionsproposal == null)
            {
                return NotFound();
            }

            _context.Priceandconditionsproposals.Remove(priceandconditionsproposal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PriceandconditionsproposalExists(string id)
        {
            return _context.Priceandconditionsproposals.Any(e => e.IdPcp == id);
        }
    }
}
