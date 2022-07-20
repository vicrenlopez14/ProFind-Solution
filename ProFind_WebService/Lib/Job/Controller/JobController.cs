using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProFind_WebService.Lib.Global.Controller;
using ProFind_WebService.Lib.Job.Model;

namespace ProFind_WebService.Lib.Job.Controller;

[Route("api/[controller]")]
[ApiController]
public class JobController : CrudController<PFJob>
{
    
    public override async Task<ActionResult<PFJob>> Get(string id)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFJob>>> List<TE>()
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFJob>>> PaginatedList(string fromIndex, string? toIndex)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFJob>>> Search(IDictionary<string, string> searchCriteria)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Create(PFJob newObject)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Update(PFJob toUpdateObject)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Delete(string id)
    {
        throw new NotImplementedException();
    }
}