using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ProFind_WebService.Lib.Global.Controller;

public abstract class CrudController<T> : ControllerBase
{
    [HttpGet("{id}")]
    public abstract Task<ActionResult<T>> Get(string id);

    [HttpGet]
    public abstract Task<ActionResult<IEnumerable<T>>> List<TE>();

    [HttpGet("{fromIndex}, {toIndex}")]
    public abstract Task<ActionResult<IEnumerable<T>>> PaginatedList(string fromIndex, string? toIndex);

    [HttpGet("{searchCriteria}")]
    public abstract Task<ActionResult<IEnumerable<T>>> Search(IDictionary<string, string> searchCriteria);

    [HttpPost("{newObject}")]
    public abstract Task<ActionResult<HttpStatusCode>> Create(T newObject);

    [HttpPut("{toUpdateObject}")]
    public abstract Task<ActionResult<HttpStatusCode>> Update(T toUpdateObject);

    [HttpDelete("{id}")]
    public abstract Task<ActionResult<HttpStatusCode>> Delete(string id);
}