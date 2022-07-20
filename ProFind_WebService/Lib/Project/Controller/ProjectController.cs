using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProFind_WebService.Lib.Global.Controller;
using ProFind_WebService.Lib.Project.Model;

namespace ProFind_WebService.Lib.Project.Controller;

[Route("api/[controller]")]
public class ProjectController : CrudController<PFProject>
{
    public override async Task<ActionResult<PFProject>> Get(string id)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFProject>>> List<TE>()
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFProject>>> PaginatedList(string fromIndex, string? toIndex)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFProject>>> Search(IDictionary<string, string> searchCriteria)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Create(PFProject newObject)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Update(PFProject toUpdateObject)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Delete(string id)
    {
        throw new NotImplementedException();
    }
}