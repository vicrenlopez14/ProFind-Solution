using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProFind_WebService.Lib.Curriculum.Model;
using ProFind_WebService.Lib.Global.Controller;

namespace ProFind_WebService.Lib.Curriculum.Controller;

[Route("api/[controller]")]
[ApiController]
public class CurriculumController : CrudController<PFCurriculum>
{
    public override async Task<ActionResult<PFCurriculum>> Get(string id)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFCurriculum>>> List()
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFCurriculum>>> PaginatedList(int fromIndex, int? toIndex)
    {
        throw new NotImplementedException();
    }

    [HttpGet("criteria")]
    public  async Task<ActionResult<IEnumerable<PFCurriculum>>> Search(
        IDictionary<string, string> searchCriteria)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Create(PFCurriculum newObject)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Update(PFCurriculum toUpdateObject)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Delete(string id)
    {
        throw new NotImplementedException();
    }
}