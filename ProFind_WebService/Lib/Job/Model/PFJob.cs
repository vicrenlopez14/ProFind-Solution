using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProFind_WebService.Lib.Job.Model;

public class PFJob
{
    [JsonConstructor]
    public PFJob(string id, string? name)
    {
        Id = id;
        Name = name;
    }

    [Column("Id_J")] public string Id { get; set; }
    [Column("Name_J")] public string? Name { get; set; }
}