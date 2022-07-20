using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ProFind_WebService.Lib.User.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpDelete("{id}")]
    public async Task<HttpStatusCode> DeleteUser(int id)
    {
        return HttpStatusCode.NotFound;
    }
}