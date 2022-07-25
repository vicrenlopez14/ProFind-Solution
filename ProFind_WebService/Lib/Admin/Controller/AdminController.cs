using System.Net;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using ProFind_WebService.Lib.Admin.DataSource;
using ProFind_WebService.Lib.Admin.Model;
using ProFind_WebService.Lib.Global.Controller;

namespace ProFind_WebService.Lib.Admin.Controller;

[Route("api/[controller]")]
[ApiController]
public class AdminController : CrudController<PFAdmin>
{
    private readonly AdminDataSource _dataSource = new();

    public override async Task<ActionResult<PFAdmin>> Get(string id)
    {
        PFAdmin? admin = await _dataSource.Get(id);
        return admin == null ? NotFound() : admin;
    }

    public override async Task<ActionResult<IEnumerable<PFAdmin>>> List()
    {
        var result = await _dataSource.List();
        return Ok(result);
    }

    public override Task<ActionResult<IEnumerable<PFAdmin>>> PaginatedList(int fromIndex, int? toIndex)
    {
        if (toIndex is null or -1) toIndex = fromIndex + 10;
        var result = _dataSource.PaginatedList(fromIndex, toIndex ?? -1);
        return Task.FromResult<ActionResult<IEnumerable<PFAdmin>>>(Ok(result));
    }

    [HttpGet("criteria")]
    public async Task<ActionResult<IEnumerable<PFAdmin>>> Search(string name)
    {
        Dictionary<string, string> searchCriteria = new()
        {
            ["Name"] = name
        };

        // Simple search
        var result = await _dataSource.Search(searchCriteria);
        return new ActionResult<IEnumerable<PFAdmin>>(Ok(result));
    }

    public override async Task<ActionResult<HttpStatusCode>> Create(PFAdmin newObject)
    {
        newObject.Id = Nanoid.Nanoid.Generate();

        return (await _dataSource.Create(newObject) ? Ok(newObject) : NotFound());
    }

    public override async Task<ActionResult<HttpStatusCode>> Update(PFAdmin toUpdateObject)
    {
        return (await _dataSource.Update(toUpdateObject) ? Ok(toUpdateObject) : NotFound());
    }

    public override async Task<ActionResult<HttpStatusCode>> Delete(string id)
    {
        var admin = new PFAdmin(id);
        return (await _dataSource.Delete(admin) ? Ok(admin) : NotFound());
    }
}