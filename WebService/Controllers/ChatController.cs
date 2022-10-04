using Azure.Communication;
using Azure.Communication.Chat;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models.Generated;
using WebService.Utils;
using Admin = WebService.Models.Generated.Admin;

namespace WebService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
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

    [HttpPost("chat/{token}")]
    public async Task<ActionResult<string>> GetThreadIdForChat([FromQuery] string professionalId,
        [FromQuery] string clientId, string token)
    {
        // If not exists, create it
        if (!ThreadExists(clientId, professionalId))
        {
            if (!ClientExists(clientId) || !ProfessionalExists(professionalId))
            {
                return NotFound();
            }

            var requiredProfessional = _context.Professionals.Find(professionalId);
            var requiredClient = _context.Clients.Find(clientId);

            var professionalParticipant =
                new ChatParticipant(
                    identifier: new CommunicationUserIdentifier(id: requiredProfessional.CommunicationIdP))
                {
                    DisplayName = requiredProfessional.NameP
                };

            var clientParticipant =
                new ChatParticipant(identifier: new CommunicationUserIdentifier(id: requiredClient.CommunicationIdC))
                {
                    DisplayName = requiredClient.NameC
                };

            var chatClient = Utils.TokensGeneratorIssuer.CreateChatClient(token);
            // Create chat
            CreateChatThreadResult createChatThreadResult =
                await chatClient.CreateChatThreadAsync(topic: "Hello world!",
                    participants: new[] { professionalParticipant, clientParticipant });
            ChatThreadClient chatThreadClient =
                chatClient.GetChatThreadClient(threadId: createChatThreadResult.ChatThread.Id);

            return chatThreadClient.Id;
        }

        // Return the thread id
        var thread = _context.Threadids
            .FirstOrDefault(t => t.IdC == clientId && t.IdP == professionalId);

        return thread.IdT;
    }

    private bool ThreadExists(string clientId, string professionalId)
    {
        return (_context.Threadids?.Any(e => e.IdC == clientId && e.IdP == professionalId)).GetValueOrDefault();
    }

    private bool ClientExists(string id)
    {
        return (_context.Clients?.Any(e => e.IdC == id)).GetValueOrDefault();
    }

    private bool ProfessionalExists(string id)
    {
        return _context.Professionals.Any(e => e.IdP == id);
    }
}