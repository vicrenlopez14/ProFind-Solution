using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProFind_WebService.Lib.Client.Model;

public class PFClient
{
    [JsonConstructor]
    public PFClient(string id, string? name = null, string? email = null, string? password = null)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }

    [Column("Id_C")] public string Id { get; set; }
    [Column("Name_C")] public string? Name { get; set; }
    [Column("Email_C")] public string? Email { get; set; }
    [Column("Password_C")] public string? Password { get; set; }
}