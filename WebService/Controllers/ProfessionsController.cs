﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using Profession = WebService.Models.Generated.Profession;

namespace WebService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfessionsController : ControllerBase
{
    private readonly ProFindContext _context;

    public ProfessionsController(ProFindContext context)
    {
        _context = context;
    }

    // GET: api/Professions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Profession>>> GetProfessions()
    {
        if (_context.Professions == null)
        {
            return NotFound();
        }
        return await _context.Professions.ToListAsync();
    }

    // GET: api/Professions/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Profession>> GetProfession(int id)
    {
        if (_context.Professions == null)
        {
            return NotFound();
        }
        var profession = await _context.Professions.FindAsync(id);

        if (profession == null)
        {
            return NotFound();
        }

        return profession;
    }

    // PUT: api/Professions/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProfession(int id, Profession profession)
    {
        if (id != profession.IdPfs)
        {
            return BadRequest();
        }

        _context.Entry(profession).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProfessionExists(id))
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

    // POST: api/Professions
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Profession>> PostProfession(Profession profession)
    {
        if (_context.Professions == null)
        {
            return Problem("Entity set 'ProFindContext.Professions'  is null.");
        }
        _context.Professions.Add(profession);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProfession", new { id = profession.IdPfs }, profession);
    }

    // DELETE: api/Professions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfession(int id)
    {
        if (_context.Professions == null)
        {
            return NotFound();
        }
        var profession = await _context.Professions.FindAsync(id);
        if (profession == null)
        {
            return NotFound();
        }

        _context.Professions.Remove(profession);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProfessionExists(int id)
    {
        return (_context.Professions?.Any(e => e.IdPfs == id)).GetValueOrDefault();
    }
}