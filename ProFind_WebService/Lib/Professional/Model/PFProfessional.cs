using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ProFind_WebService.Lib.Curriculum.Model;
using ProFind_WebService.Lib.Job.Model;

namespace ProFind_WebService.Lib.Professional.Model;

public class PFProfessional
{
    [JsonConstructor]
    public PFProfessional(string id, string? name, DateTime? dateBirth, string? email, string? password,
        string? curriculumId, string? jobId)
    {
        Id = id;
        Name = name;
        DateBirth = dateBirth;
        Email = email;
        Password = password;
        CurriculumId = curriculumId;
        JobId = jobId;
    }

    [Column("Id_P")] public string Id { get; set; }
    [Column("Name_P")] public string? Name { get; set; }
    [Column("Date_Birth_P")] public DateTime? DateBirth { get; set; }
    [Column("Email_P")] public string? Email { get; set; }
    [Column("Password_P")] public string? Password { get; set; }
    [Column("Id_CU1")] public string? CurriculumId { get; set; }
    [Column("Id_J1")] public string? JobId { get; set; }
}