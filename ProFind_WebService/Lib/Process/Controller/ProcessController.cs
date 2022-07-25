using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProFind_WebService.Lib.Global.Controller;
using ProFind_WebService.Lib.Process.Model;

namespace ProFind_WebService.Lib.Process.Controller;

[Route("api/[controller]")]
[ApiController]
public class ProcessController : CrudController<PFProcess>
{
    [HttpGet("{id}")]
    public override async Task<ActionResult<PFProcess>> Get(string id)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public override async Task<ActionResult<IEnumerable<PFProcess>>> List()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{fromIndex, toIndex}")]
    public override async Task<ActionResult<IEnumerable<PFProcess>>> PaginatedList(int fromIndex, int? toIndex)
    {
        throw new NotImplementedException();
    }

    [HttpGet("criteria")]
    public  async Task<ActionResult<IEnumerable<PFProcess>>> Search(IDictionary<string, string> searchCriteria)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Create(PFProcess newObject)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Update(PFProcess toUpdateObject)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Delete(string id)
    {
        throw new NotImplementedException();
    }
}