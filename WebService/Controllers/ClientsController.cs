using Azure.Communication.Email.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;
using WebService.Utils;
using Client = WebService.Models.Generated.Client;

namespace WebService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly ProFindContext _context;

    public ClientsController(ProFindContext context)
    {
        _context = context;
    }

    // Register a client user method
    // POST: api/Clients/Register
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterClient(Client client)
    {
        if (ModelState.IsValid)
        {
            client.AssignId();
            client.PasswordC = ShaOperations.ShaPassword(client.PasswordC);
            client.RegistrationDateC = DateTime.Now;
            client.CommunicationIdC = TokensGeneratorIssuer.GenerateCommunicationId();
            _context.Add(client);
            await _context.SaveChangesAsync();
            return Ok(client);
        }

        return BadRequest(ModelState);
    }

    // Login a client user method
    // Login an Admin method
    // POST: api/Clients/Login
    [HttpPost("Login")]
    public async Task<IActionResult> LoginClient(ClientLogin client)
    {
        if (ModelState.IsValid)
        {
            var clientFromDb =
                await _context.Clients.FirstOrDefaultAsync(a =>
                    a.EmailC == client.Email && a.PasswordC == Utils.ShaOperations.ShaPassword(client.Password));
            if (clientFromDb != null)
            {
                return Ok(clientFromDb);
            }
        }

        return BadRequest(ModelState);
    }

    // GET: api/Clients
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetClients()
    {
        if (_context.Clients == null)
        {
            return NotFound();
        }

        return await _context.Clients.ToListAsync();
    }

    // POST: api/Admins/SendRecoveryEmail
    [HttpPost("SendRecoveryEmail")]
    public async Task<IActionResult> SendRecoveryEmailClients(string email)
    {
        if (ModelState.IsValid)
        {
            var clientFromDb = await _context.Clients.FirstOrDefaultAsync(a => a.EmailC == email);
            if (clientFromDb != null)
            {
                // Make any other code for this user invalid
                var otherCodes =
                    await _context.Changepasswordcodes.Where(c => c.IdC1 == clientFromDb.IdC).ToListAsync();

                foreach (var otherCode in otherCodes)
                {
                    otherCode.ValidCpc = false;
                    _context.Update(otherCode);
                }

                // Creation of the code to be sent to the user
                // Random code with 4 digits
                var random = new Random();
                var verificationCode = random.Next(1000, 9999).ToString();
                _context.Changepasswordcodes.Add(new Changepasswordcode
                {
                    IdCpc = verificationCode,
                    IdC1 = clientFromDb.IdC, // Id of the admin
                    ValidCpc = true, // Made invalid once the code is used
                });

                // Send email
                EmailContent emailContent = new EmailContent("Password recovery");
                emailContent.Html =
                    $"In order to recover your password, please enter this code in the app: {verificationCode}";
                List<EmailAddress> emailAddresses = new List<EmailAddress>
                    { new EmailAddress(clientFromDb.EmailC) { DisplayName = clientFromDb.NameC } };
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
    public async Task<IActionResult> VerifyRecoveryCodeClients(string code)
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

    // GET: api/Clients/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClient(string id)
    {
        if (_context.Clients == null)
        {
            return NotFound();
        }

        var client = await _context.Clients.FindAsync(id);

        if (client == null)
        {
            return NotFound();
        }

        return client;
    }

    // Get from email
    // GET: api/Clients/5
    [HttpGet("GetByEmail/{email}")]
    public async Task<ActionResult<Client>> GetClientByEmail(string email)
    {
        if (_context.Clients == null)
        {
            return NotFound();
        }

        var client = await _context.Clients.FirstOrDefaultAsync(a => a.EmailC == email);

        if (client == null)
        {
            return NotFound();
        }

        return client;
    }

    // Change password
    // POST: api/Clients/ChangePassword
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePasswordClient(string email, string password)
    {
        if (ModelState.IsValid)
        {
            var clientFromDb = await _context.Clients.FirstOrDefaultAsync(a => a.EmailC == email);
            if (clientFromDb != null)
            {
                clientFromDb.PasswordC = ShaOperations.ShaPassword(password);
                _context.Update(clientFromDb);
                await _context.SaveChangesAsync();
                return Ok("Password changed successfully.");
            }
        }

        return BadRequest(ModelState);
    }

    // PUT: api/Clients/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutClient(string id, Client client)
    {
        if (id != client.IdC)
        {
            return BadRequest();
        }

        if (client.PasswordC.Length < 50)
        {
            client.PasswordC = ShaOperations.ShaPassword(client.PasswordC);
        }

        _context.Entry(client).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClientExists(id))
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

    // POST: api/Clients
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Client>> PostClient(Client client)
    {
        if (_context.Clients == null)
        {
            return Problem("Entity set 'ProFindContext.Clients'  is null.");
        }

        client.AssignId();
        client.PasswordC = Utils.ShaOperations.ShaPassword(client.PasswordC);
        client.RegistrationDateC = DateTime.Now;
        client.CommunicationIdC = TokensGeneratorIssuer.GenerateCommunicationId();

        _context.Clients.Add(client);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (ClientExists(client.IdC))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetClient", new { id = client.IdC }, client);
    }

    // DELETE: api/Clients/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(string id)
    {
        if (_context.Clients == null)
        {
            return NotFound();
        }

        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ClientExists(string id)
    {
        return (_context.Clients?.Any(e => e.IdC == id)).GetValueOrDefault();
    }

    [HttpGet("search/")]
    public async Task<ActionResult<IEnumerable<Client>>> SearchCliens([FromQuery] string Name)
    {
        if (Name.Length == 0) return await GetClients();
        else
        {
            var result = await (from client in _context.Clients where (client.NameC.Contains(Name)) select client)
                .ToListAsync();

            if (result.Any() == false) return NotFound();
            else return result;
        }
    }


    [HttpGet("search/paginated")]
    public async Task<ActionResult<IEnumerable<Client>>> SearchClient([FromQuery] string idC,
        [FromQuery] string Name, [FromQuery] string limit,
        [FromQuery] string offset)
    {
        var query = (from Client in _context.Clients
            where (Client.IdC == idC && Client.NameC.Contains(Name))
            select Client);

        query = (from Client in _context.Clients select Client).OrderByDescending(x => x.NameC)
            .Skip(int.Parse(offset)).Take(int.Parse(limit));

        var result = await query.ToListAsync();

        if (result.Any() == false)
        {
            return NotFound();
        }

        return result;
    }

    [HttpGet("filter/")]
    public async Task<ActionResult<IEnumerable<Client>>> FilterClients([FromQuery] string Name,
        [FromQuery] string? name1,
        [FromQuery] string? idclien)

    {
        var query = _context.Clients.Where(act => true);

        if (idclien != null)
        {
            query = _context.Clients.Where(act => act.IdC == idclien);
        }

        if (Name != null)
        {
            query = _context.Clients.Where(act => act.NameC == name1);
        }


        var result = await query.ToListAsync();

        if (result.Any() == false)
        {
            return NotFound();
        }

        return result;
    }

    [HttpGet("paginated")]
    public async Task<ActionResult<IEnumerable<Client>>> GetClientPaginated([FromQuery] string limit,
        [FromQuery] string offset)
    {
        var query = (from Client in _context.Clients select Client).OrderByDescending(x => x.NameC)
            .Skip(int.Parse(offset)).Take(int.Parse(limit));

        return await _context.Clients.ToListAsync();
    }
}