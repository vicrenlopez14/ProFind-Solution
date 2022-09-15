using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebService.Data;
using WebService.Models.Generated;

namespace WebService.Controllers;

public class AdminLogin
{
    public string Email { get; set; }
    public string Password { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class LoginAdminController : ControllerBase
{
    private readonly ProFindContext _context;
    private readonly IConfiguration _config;

    public LoginAdminController(IConfiguration config, ProFindContext context)
    {
        _config = config;
        _context = context;
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult Login([FromBody] AdminLogin adminLogin)
    {
        var admin = Authenticate(adminLogin);
        if (admin != null)
        {
            var token = GenerateToken(admin);
            return Ok(token);
        }

        return NotFound("user not found");
    }

    private string GenerateToken(Admin admin)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, admin.EmailA!),
            new Claim(ClaimTypes.Role, admin.GetType().Name)
        };
        var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims,
            expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private Admin Authenticate(AdminLogin adminLogin)
    {
        var currentUser = _context.Admins.FirstOrDefault(x =>
            x.EmailA!.ToLower() == adminLogin.Email.ToLower() &&
            x.PasswordA == Utils.ShaOperations.ShaPassword(adminLogin.Password));

        return currentUser!;
    }
}