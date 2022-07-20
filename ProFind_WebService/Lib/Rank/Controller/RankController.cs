using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProFind_WebService.Lib.Global.Controller;
using ProFind_WebService.Lib.Rank.Model;

namespace ProFind_WebService.Lib.Rank.Controller;

[Route("api/[controller]")]
[ApiController]
public class RankController : CrudController<PFRank>
{
    public override async Task<ActionResult<PFRank>> Get(string id)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFRank>>> List<TE>()
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFRank>>> PaginatedList(string fromIndex, string? toIndex)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFRank>>> Search(IDictionary<string, string> searchCriteria)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Create(PFRank newObject)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Update(PFRank toUpdateObject)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Delete(string id)
    {
        throw new NotImplementedException();
    }
}