using Application.Interfaces;
using ProFind_WebService.Lib.Rank.Model;

namespace ProFind_WebService.Lib.Admin.Model;

public class PFAdmin : DedictionarizableEntity<PFAdmin>
{
    private DedictionarizableEntity<PFAdmin> _dedictionarizableEntityImplementation;

    public static PFAdmin FromDictionary(IDictionary<string, object> dictionary)
    {
        return new PFAdmin(dictionary["Id_A"].ToString(), dictionary["Name_A"].ToString(), dictionary["Email_A"].ToString(),
            dictionary["Tel_A"].ToString(), dictionary["Password_A"].ToString(),
            PFRank.FromDictionary(dictionary));
    }

    public static IEnumerable<PFAdmin> FromDictionary(IEnumerable<IDictionary<string, object>> dictionaries)
        => dictionaries.Select(FromDictionary).ToList();


    public PFAdmin(string id)
    {
        Id = id;
    }
    public PFAdmin(string id, string name, string email, string tel, string password, PFRank rank)
    {
        Id = id;
        Name = name;
        Email = email;
        Tel = tel;
        Password = password;
        Rank = rank;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Tel { get; set; }
    public string Password { get; set; }
    public PFRank Rank { get; set; }

}