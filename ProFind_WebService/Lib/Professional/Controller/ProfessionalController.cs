using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProFind_WebService.Lib.Global.Controller;
using ProFind_WebService.Lib.Professional.Model;

namespace ProFind_WebService.Lib.Professional.Controller;

[Route("api/[controller]")]
[ApiController]
public class ProfessionalController : CrudController<PFProfessional>
{
    public override async Task<ActionResult<PFProfessional>> Get(string id)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFProfessional>>> List()
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFProfessional>>> PaginatedList(int fromIndex,
        int? toIndex)
    {
        throw new NotImplementedException();
    }

    [HttpGet("criteria")]
    public async Task<ActionResult<IEnumerable<PFProfessional>>> Search(
        IDictionary<string, string> searchCriteria)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Create(PFProfessional newObject)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Update(PFProfessional toUpdateObject)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Delete(string id)
    {
        throw new NotImplementedException();
    }
}