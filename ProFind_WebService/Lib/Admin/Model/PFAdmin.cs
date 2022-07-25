using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProFind_WebService.Lib.Admin.Model;

public class PFAdmin
{
    [JsonConstructor]
    public PFAdmin(string id = null, string? name = null, string? email = null, string? tel = null,
        string? password = null, string? rankId = null)
    {
        Id = id;
        Name = name;
        Email = email;
        Tel = tel;
        Password = password;
        RankId = rankId;
    }

    [Column("Id_A")] public string Id { get; set; }
    [Column("Name_A")] public string? Name { get; set; }
    [Column("Email_A")] public string? Email { get; set; }
    [Column("Tel_A")] public string? Tel { get; set; }
    [Column("Password_A")] public string? Password { get; set; }
    [Column("Id_R1")] public string? RankId { get; set; }
}