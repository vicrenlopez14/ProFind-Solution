using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;
using WebService.Utils;
using Admin = WebService.Models.Generated.Admin;

namespace WebService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminsController : ControllerBase
{
    private readonly ProFindContext _context;

    public AdminsController(ProFindContext context)
    {
        _context = context;
    }

    // Register an Admin method
    // POST: api/Admins/Register
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAdmin(Admin admin)
    {
        if (ModelState.IsValid)
        {
            admin.AssignId();
            admin.PasswordA = ShaOperations.ShaPassword(admin.PasswordA);
            admin.CommunicationIdA = TokensGeneratorIssuer.GenerateCommunicationId();

            _context.Add(admin);
            await _context.SaveChangesAsync();
            return Ok(admin);
        }

        return BadRequest(ModelState);
    }


    // Login an Admin method
    // POST: api/Admins/Login
    [HttpPost("Login")]
    public async Task<IActionResult> LoginAdmin(AdminLogin admin)
    {
        if (ModelState.IsValid)
        {
            var hashedPassword = ShaOperations.ShaPassword(admin.Password);
            var adminFromDb =
                await _context.Admins.FirstOrDefaultAsync(a =>
                    a.EmailA == admin.Email && a.PasswordA == hashedPassword);
            if (adminFromDb != null)
            {
                return Ok(adminFromDb);
            }
        }

        return BadRequest(ModelState);
    }

    // POST: api/Admins/SendRecoveryEmail
    [HttpPost("SendRecoveryEmail")]
    public async Task<IActionResult> SendRecoveryEmailAdmins(string email)
    {
        if (ModelState.IsValid)
        {
            var adminFromDb = await _context.Admins.FirstOrDefaultAsync(a => a.EmailA == email);
            if (adminFromDb != null)
            {
                // Creation of the code to be sent to the user
                // Random code with 4 digits
                var random = new Random();
                var verificationCode = random.Next(1000, 9999).ToString();
                _context.Changepasswordcodes.Add(new Changepasswordcode
                {
                    IdCpc = verificationCode,
                    IdA1 = adminFromDb.IdA, // Id of the admin
                    ValidCpc = true, // Made invalid once the code is used
                });
                await _context.SaveChangesAsync();

                // Send email
                EmailContent emailContent = new EmailContent("Password recovery");
                emailContent.Html =
                    $"In order to recover your password, please enter this code in the app: {verificationCode}";
                List<EmailAddress> emailAddresses = new List<EmailAddress>
                    { new EmailAddress(adminFromDb.EmailA) { DisplayName = adminFromDb.NameA } };
                EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
                EmailMessage emailMessage = new EmailMessage("donotreply@profind.work", emailContent, emailRecipients);
                SendEmailResult emailResult =
                    Utils.EmailClientUtil.GetEmailClient.Send(emailMessage, CancellationToken.None);

                return Ok("Password recovery code has been sent to the indicated email address.");
            }
        }

        return BadRequest(ModelState);
    }

    // Method to verify a password recovery code
    // POST: api/Admins/VerifyRecoveryCode
    [HttpPost("VerifyRecoveryCode")]
    public async Task<IActionResult> VerifyRecoveryCodeAdmins(string code)
    {
        if (ModelState.IsValid)
        {
            var codeFromDb = await _context.Changepasswordcodes.FirstOrDefaultAsync(c => c.IdCpc == code);
            if (codeFromDb != null)
            {
                if (codeFromDb.ValidCpc == true)
                {
                    codeFromDb.ValidCpc = false;
                    _context.Update(codeFromDb);
                    return Ok("Code is valid.");
                }
            }
        }

        return BadRequest(ModelState);
    }

    // GET: api/Admins
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
    {
        if (_context.Admins == null)
        {
            return NotFound();
        }

        var toIncludeAdmins = await _context.Admins.Include(x => x.IdR1Navigation).ToListAsync();
        return toIncludeAdmins;
    }

    // GET: api/Admins/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Admin>> GetAdmin(string id)
    {
        if (_context.Admins == null)
        {
            return NotFound();
        }

        var admin = await _context.Admins.FindAsync(id);

        if (admin == null)
        {
            return NotFound();
        }

        return admin;
    }

    // Get admin from email
    // GET: api/Admins/GetAdminFromEmail
    [HttpGet("GetByEmail/{email}")]
    public async Task<ActionResult<Admin>> GetAdminFromEmail(string email)
    {
        if (_context.Admins == null)
        {
            return NotFound();
        }

        var admin = await _context.Admins.FirstOrDefaultAsync(a => a.EmailA == email);

        if (admin == null)
        {
            return NotFound();
        }

        return admin;
    }

    // Change password 
    // POST: api/Admins/ChangePassword
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePasswordAdmins(string email, string password)
    {
        if (ModelState.IsValid)
        {
            var adminFromDb = await _context.Admins.FirstOrDefaultAsync(a => a.EmailA == email);
            if (adminFromDb != null)
            {
                adminFromDb.PasswordA = ShaOperations.ShaPassword(password);
                _context.Update(adminFromDb);
                await _context.SaveChangesAsync();
                return Ok("Password changed successfully.");
            }
        }

        return BadRequest(ModelState);
    }

    // PUT: api/Admins/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAdmin(string id, Admin admin)
    {
        if (id != admin.IdA)
        {
            return BadRequest();
        }

        // Check if the email is already in use
        var adminFromDb = await _context.Admins.FirstOrDefaultAsync(a => a.EmailA == admin.EmailA);
        if (adminFromDb != null)
        {
            return Problem("The email is already in use.");
        }

        // Check that name doesnt contain numbers
        if (!Utils.DataIntegrityValidations.CheckText(admin.NameA))
        {
            return Problem("The name cannot contain numbers.");
        }

        // Check email format
        if (!Utils.DataIntegrityValidations.CheckEmail(admin.EmailA))
        {
            return Problem("The email format is not valid.");
        }
        
        if (admin.PasswordA.Length < 50)
        {
            // Check that the new password is not the same
            if (adminFromDb.PasswordA == ShaOperations.ShaPassword(admin.PasswordA))
            {
                return Problem("The new password cannot be the same as the old one.");
            }
            
            admin.PasswordA = ShaOperations.ShaPassword(admin.PasswordA);
        }

        _context.Entry(admin).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AdminExists(id, admin.EmailA))
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

    // POST: api/Admins
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
    {
        if (_context.Admins == null)
        {
            return Problem("Entity set 'ProFindContext.Admins'  is null.");
        }

        // Check if the email is already in use
        var adminFromDb = await _context.Admins.FirstOrDefaultAsync(a => a.EmailA == admin.EmailA);
        if (adminFromDb != null)
        {
            return Problem("The email is already in use.");
        }

        // Check that name doesnt contain numbers
        if (admin.NameA.Any(char.IsDigit))
        {
            return Problem("The name cannot contain numbers.");
        }

        // Check email format
        if (!Utils.DataIntegrityValidations.CheckEmail(admin.EmailA))
        {
            return Problem("The email format is not valid.");
        }

        admin.AssignId();
        admin.PasswordA = ShaOperations.ShaPassword(admin.PasswordA);
        admin.CommunicationIdA = TokensGeneratorIssuer.GenerateCommunicationId();
        admin.CreationDateA = DateTime.Now;

        _context.Admins.Add(admin);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (AdminExists(admin.IdA, admin.EmailA))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetAdmin", new { id = admin.IdA }, admin);
    }

    // DELETE: api/Admins/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdmin(string id)
    {
        if (_context.Admins == null)
        {
            return NotFound();
        }

        var admin = await _context.Admins.FindAsync(id);
        if (admin == null)
        {
            return NotFound();
        }

        _context.Admins.Remove(admin);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AdminExists(string id, string email)
    {
        return (_context.Admins?.Any(e => e.IdA == id || e.EmailA == email)).GetValueOrDefault();
    }

    [HttpGet("search/")]
    public async Task<ActionResult<IEnumerable<Admin>>> SearchAdmins([FromQuery] string idA,
        [FromQuery] string Name)
    {
        var result = await (from Admins in _context.Admins
            where (Admins.IdA == idA && Admins.NameA.Contains(Name))
            select Admins).ToListAsync();

        if (result.Any() == false)
        {
            return NotFound();
        }

        return result;
    }

    [HttpGet("search/paginated")]
    public async Task<ActionResult<IEnumerable<Admin>>> SearchAdminsPaginated([FromQuery] string idA,
        [FromQuery] string Name, [FromQuery] string limit,
        [FromQuery] string offset)
    {
        var query = (from admin in _context.Admins
            where (admin.IdA == idA && admin.NameA.Contains(Name))
            select admin);

        query = (from Admin in _context.Admins select Admin).OrderByDescending(x => x.NameA)
            .Skip(int.Parse(offset)).Take(int.Parse(limit));

        var result = await query.ToListAsync();

        if (result.Any() == false)
        {
            return NotFound();
        }

        return result;
    }

    [HttpGet("filter/")]
    public async Task<ActionResult<IEnumerable<Admin>>> FilterAdmins([FromQuery] string? Name,
        [FromQuery] string? Rank)

    {
        var result = await (from admin in _context.Admins
            where (
                (Name == null ? true : admin.NameA.Contains(Name)) &&
                (Rank == null ? true : admin.IdR1.ToString() == Rank))
            select admin).ToListAsync();

        if (result.Any() == false) return NotFound();
        else return result;
    }

    [HttpGet("paginated")]
    public async Task<ActionResult<IEnumerable<Admin>>> GetAdminsPaginated([FromQuery] string limit,
        [FromQuery] string offset)
    {
        var query = (from activity in _context.Admins select activity).OrderByDescending(x => x.NameA)
            .Skip(int.Parse(offset)).Take(int.Parse(limit));

        return await _context.Admins.ToListAsync();
    }
}