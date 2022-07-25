using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProFind_WebService.Lib.Rank.Model;

public class PFRank
{
    [JsonConstructor]
    public PFRank(string? id = null, string? name = null)
    {
        Id = id;
        Name = name;
    }

    [Column("Id_R")] public string? Id { get; set; }
    [Column("Name_R")] public string? Name { get; set; }
}