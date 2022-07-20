using Application.Interfaces;

namespace ProFind_WebService.Lib.Client.Model;

public class PFClient : DedictionarizableEntity<PFClient>
{
    private DedictionarizableEntity<PFClient> _dedictionarizableEntityImplementation;

    public static PFClient FromDictionary(IDictionary<string, object> dictionary)
    {
        return new PFClient(dictionary["Id_C"].ToString(), dictionary["Name_C"].ToString(),
            dictionary["Email_C"].ToString(), dictionary["Password_C"].ToString(), dictionary["CreditCard_C"].ToString());
    }

    public static IEnumerable<PFClient> FromDictionary(IEnumerable<IDictionary<string, object>> dictionaries)
        => dictionaries.Select(FromDictionary).ToList();

    public PFClient(string id, string name, string email, string password, string creditcard)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        CreditCard = creditcard;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string CreditCard { get; set; }
}