using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProFind_WebService.Lib.Admin.DataSource;
using ProFind_WebService.Lib.Admin.Model;
using ProFind_WebService.Lib.Global.Controller;

namespace ProFind_WebService.Lib.Admin.Controller;

[Route("api/[controller]")]
[ApiController]
public class AdminController : CrudController<PFAdmin>
{
    private readonly AdminDataSource dataSource = new();

    [HttpGet("{id}")]
    public async Task<ActionResult<PFAdmin>> GetAdmin(string id)
    {
        PFAdmin admin = await dataSource.Get(id);
        return admin == null ? NotFound() : admin;
    }

    [HttpPost("")]
    public async Task<ActionResult> PostAdmin(PFAdmin admin)
    {
        return (await dataSource.Create(admin) ? Ok(admin) : NotFound());
    }

    [HttpPut("")]
    public async Task<ActionResult> PutAdmin(PFAdmin admin)
    {
        return (await dataSource.Update(admin) ? Ok(admin) : NotFound());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<PFAdmin>> DeleteAdmin(string id)
    {
        PFAdmin admin = new PFAdmin(id);
        return (await dataSource.Delete(admin) ? admin : NotFound());
    }

    public override async Task<ActionResult<PFAdmin>> Get(string id)
    {
        PFAdmin admin = await dataSource.Get(id);

        return admin;
    }

    public override async Task<ActionResult<IEnumerable<PFAdmin>>> List<TE>()
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFAdmin>>> PaginatedList(string fromIndex, string? toIndex)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<IEnumerable<PFAdmin>>> Search(IDictionary<string, string> searchCriteria)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Create(PFAdmin newObject)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Update(PFAdmin toUpdateObject)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult<HttpStatusCode>> Delete(string id)
    {
        throw new NotImplementedException();
    }
}