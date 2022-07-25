using System.ComponentModel.DataAnnotations.Schema;

namespace ProFind_WebService.Lib.Tag.Model;

public class PFTag
{
    public PFTag(string id, string? name = null, string? tagId = null)
    {
        Id = id;
        Name = name;
        TagId = tagId;
    }

    [Column("Id_T")] public string Id { get; set; }
    [Column("Name_T")] public string? Name { get; set; }
    [Column("Id_PJ1")] public string? TagId { get; set; }
}