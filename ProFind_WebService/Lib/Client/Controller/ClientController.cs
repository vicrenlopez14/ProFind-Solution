using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProFind_WebService.Lib.Client.Model;
using ProFind_WebService.Lib.Global.Controller;

namespace ProFind_WebService.Lib.Client.Controller;

[Route("api/[controller]")]
[ApiController]
public class ClientController : CrudController<PFClient>
{
    public override async Task<ActionResult<PFClient>> Get(string id)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFClient>>> List<TE>()
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFClient>>> PaginatedList(string fromIndex, string? toIndex)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFClient>>> Search(IDictionary<string, string> searchCriteria)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Create(PFClient newObject)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Update(PFClient toUpdateObject)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Delete(string id)
    {
        throw new NotImplementedException();
    }
}