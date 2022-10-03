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
public class ChatController
{
    private readonly ProFindContext _context;

    public ChatController(ProFindContext context)
    {
        _context = context;
    }

    // Get chat access information
    [HttpGet("{communicationId}")]
    public async Task<ActionResult<ChatAccessInfo>> GetChatAccessInfo(string communicationId)
    {
        return Utils.TokensGeneratorIssuer.IssueToken(communicationId);
    }
}