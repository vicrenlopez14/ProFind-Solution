using Application.Interfaces;
using ProFind_WebService.Lib.Curriculum.Model;
using ProFind_WebService.Lib.Job.Model;

namespace ProFind_WebService.Lib.Professional.Model;

public class PFProfessional : DedictionarizableEntity<PFProfessional>
{

    public static PFProfessional FromDictionary(IDictionary<string, object> dictionary)
    {
        return new PFProfessional(dictionary["Id_P"].ToString(), dictionary["Name_P"].ToString(),
            DateTime.Parse(dictionary["Date_Birth_P"].ToString()), dictionary["Email_P"].ToString(),
            dictionary["Password_P"].ToString(), PFCurriculum.FromDictionary(dictionary), PFJob.FromDictionary(dictionary));
    }

    public static IEnumerable<PFProfessional> FromDictionary(IEnumerable<IDictionary<string, object>> dictionaries)
        => dictionaries.Select(FromDictionary).ToList();


    public PFProfessional(string id, string name, DateTime datebirth, string email, string password,
        PFCurriculum curriculum, PFJob job)
    {
        Id = id;
        Name = name;
        DateBirth = datebirth;
        Email = email;
        Password = password;
        Curriculum = curriculum;
        Job = job;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime DateBirth { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public PFCurriculum Curriculum { get; set; }
    public PFJob Job { get; set; }

}