using System.Text.Json.Serialization;

namespace ProFind_WebService.Lib.Project.Model;

public class PFProject
{
    [JsonConstructor]
    public PFProject(string id, string? title = null, string? description = null)
    {
        Id = id;
        Title = title;
        Description = description;
    }


    public string Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}