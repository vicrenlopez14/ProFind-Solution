using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Communication.Email.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;
using WebService.Utils;

namespace WebService.Controllers
{
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
                professional.AssignId();
                professional.PasswordP = ShaOperations.ShaPassword(professional.PasswordP);
                professional.CommunicationIdP = TokensGeneratorIssuer.GenerateCommunicationId();

                _context.Add(professional);
                await _context.SaveChangesAsync();
                return Ok(professional);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        // POST: api/Professionals/Login
        public async Task<IActionResult> LoginProfessional(ProfessionalLogin professional)
        {
            if (ModelState.IsValid)
            {
                var professionalFromDb =
                    await _context.Professionals.FirstOrDefaultAsync(a =>
                        a.EmailP == professional.Email &&
                        a.PasswordP == Utils.ShaOperations.ShaPassword(professional.Password));
                if (professionalFromDb != null)
                {
                    return Ok(professionalFromDb);
                }
            }

            return BadRequest(ModelState);
        }

        // Get from email
        [HttpGet("GetByEmail/{email}")]
        public async Task<ActionResult<Professional>> GetProfessionalByEmail(string email)
        {
            if (_context.Professionals == null)
            {
                return NotFound();
            }

            var professional = await _context.Professionals.FirstOrDefaultAsync(a => a.EmailP == email);

            if (professional == null)
            {
                return NotFound();
            }

            return professional;
        }

        [HttpGet("GetByCommunicationId/{email}")]
        public async Task<ActionResult<Professional>> GetProfessionalByCommunicationId(string communicationId)
        {
            if (_context.Professionals == null)
            {
                return NotFound();
            }

            var professional =
                await _context.Professionals.FirstOrDefaultAsync(a => a.CommunicationIdP == communicationId);

            if (professional == null)
            {
                return NotFound();
            }

            return professional;
        }

        // Change password
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var professionalFromDb =
                    await _context.Professionals.FirstOrDefaultAsync(a => a.EmailP == email);
                if (professionalFromDb != null)
                {
                    professionalFromDb.PasswordP = ShaOperations.ShaPassword(password);
                    _context.Update(professionalFromDb);
                    await _context.SaveChangesAsync();
                    return Ok(professionalFromDb);
                }
            }

            return BadRequest(ModelState);
        }


        // POST: api/Admins/SendRecoveryEmail
        [HttpPost("SendRecoveryEmail")]
        public async Task<IActionResult> SendRecoveryEmailProfessionals(string email)
        {
            if (ModelState.IsValid)
            {
                var professionalFromDb = await _context.Professionals.FirstOrDefaultAsync(a => a.EmailP == email);
                if (professionalFromDb != null)
                {
                    // Creation of the code to be sent to the user
                    // Random code with 4 digits
                    var random = new Random();
                    var verificationCode = random.Next(1000, 9999).ToString();
                    _context.Changepasswordcodes.Add(new Changepasswordcode
                    {
                        IdCpc = verificationCode,
                        IdP1 = professionalFromDb.IdP, // Id of the admin
                        ValidCpc = true, // Made invalid once the code is used
                    });

                    // Send email
                    EmailContent emailContent = new EmailContent("Password recovery");
                    emailContent.Html =
                        $"In order to recover your password, please enter this code in the app: {verificationCode}";
                    List<EmailAddress> emailAddresses = new List<EmailAddress>
                        { new EmailAddress(professionalFromDb.EmailP) { DisplayName = professionalFromDb.NameP } };
                    EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
                    EmailMessage emailMessage =
                        new EmailMessage("donotreply@profind.work", emailContent, emailRecipients);
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
        public async Task<IActionResult> VerifyRecoveryCodeProfessionals(string code)
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

        [HttpGet("filter/")]
        public async Task<ActionResult<IEnumerable<Professional>>> FilterProfessionals([FromQuery] string? professionId,
            [FromQuery] string? departmentId,
            [FromQuery] string? name)
        {
            var result = await (from prof in _context.Professionals
                    where ((professionId == null || prof.IdDp1.ToString() == departmentId) &&
                           (name == null || prof.NameP.Contains(name))
                           && (departmentId == null || prof.IdPfs1.ToString() == departmentId))
                    select prof).Include(x => professionId != null ? x.IdPfs1Navigation : null)
                .Include(x => departmentId != null ? x.IdDp1Navigation : null).ToListAsync();

            if (result.Any() == false)
            {
                return NotFound();
            }

            return result;
        }

        [HttpGet("search/")]
        public async Task<ActionResult<IEnumerable<Professional>>> SearchProfessionals([FromQuery] string name)
        {
            if (name.Length == 0)
            {
                return await GetProfessionals();
            }
            else
            {
                var result = await (from prof in _context.Professionals where (prof.NameP.Contains(name)) select prof)
                    .ToListAsync();
                if (result.Any() == false) return NotFound();
                else return result;
            }
        }

        // GET: api/Professionals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professional>>> GetProfessionals()
        {
            return await _context.Professionals.Include(x => x.IdDp1Navigation)
                .Include(x => x.IdPfs1Navigation).ToListAsync();
        }

        // GET: api/Professionals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Professional>> GetProfessional(string id)
        {
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

            if (professional.PasswordP.Length < 50)
                professional.PasswordP = ShaOperations.ShaPassword(professional.PasswordP);
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
            professional.AssignId();
            professional.PasswordP = ShaOperations.ShaPassword(professional.PasswordP);
            professional.CommunicationIdP = TokensGeneratorIssuer.GenerateCommunicationId();

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

        // DELETE: api/Professionals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfessional(string id)
        {
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
            return _context.Professionals.Any(e => e.IdP == id);
        }
    }
}