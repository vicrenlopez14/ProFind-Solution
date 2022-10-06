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
    public class GraphicpreferencesController : ControllerBase
    {
        private readonly ProFindContext _context;

        public GraphicpreferencesController(ProFindContext context)
        {
            _context = context;
        }

        // GET: api/Graphicpreferences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Graphicpreference>>> GetGraphicpreferences()
        {
            return await _context.Graphicpreferences.ToListAsync();
        }

        // GET: api/Graphicpreferences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Graphicpreference>> GetGraphicpreference(string id)
        {
            var graphicpreference = await _context.Graphicpreferences.FindAsync(id);

            if (graphicpreference == null)
            {
                return NotFound();
            }

            return graphicpreference;
        }

        // PUT: api/Graphicpreferences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGraphicpreference(string id, Graphicpreference graphicpreference)
        {
            if (id != graphicpreference.IdPrf)
            {
                return BadRequest();
            }

            _context.Entry(graphicpreference).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GraphicpreferenceExists(id))
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

        // POST: api/Graphicpreferences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Graphicpreference>> PostGraphicpreference(Graphicpreference graphicpreference)
        {
            _context.Graphicpreferences.Add(graphicpreference);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GraphicpreferenceExists(graphicpreference.IdPrf))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGraphicpreference", new { id = graphicpreference.IdPrf }, graphicpreference);
        }

        // DELETE: api/Graphicpreferences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGraphicpreference(string id)
        {
            var graphicpreference = await _context.Graphicpreferences.FindAsync(id);
            if (graphicpreference == null)
            {
                return NotFound();
            }

            _context.Graphicpreferences.Remove(graphicpreference);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GraphicpreferenceExists(string id)
        {
            return _context.Graphicpreferences.Any(e => e.IdPrf == id);
        }
    }
}
