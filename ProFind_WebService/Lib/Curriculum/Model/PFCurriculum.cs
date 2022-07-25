using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MySqlX.XDevAPI.Relational;

namespace ProFind_WebService.Lib.Curriculum.Model;

public class PFCurriculum
{
    [JsonConstructor]
    public PFCurriculum(string id, string? studies = null, string? experiences = null, int? years = default)
    {
        Id = id;
        Studies = studies;
        Experiences = experiences;
        Years = years;
    }

    [Column("Id_CU")] public string Id { get; set; }
    [Column("Studies_CU")] public string? Studies { get; set; }
    [Column("Experiences_CU")] public string? Experiences { get; set; }
    [Column("Years_CU")] public int? Years { get; set; }
}