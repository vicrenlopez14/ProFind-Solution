using Application.Interfaces;

namespace ProFind_WebService.Lib.Rank.Model;

public class PFRank : DedictionarizableEntity<PFRank>
{
    private DedictionarizableEntity<PFRank> _dedictionarizableEntityImplementation;

    public static PFRank FromDictionary(IDictionary<string,object> dictionary)
    {
        return new PFRank(dictionary["Id_R"].ToString(), dictionary["Name_R"].ToString());
    }

    public static IEnumerable<PFRank> FromDictionary(IEnumerable<IDictionary<string,object>> dictionaries)
        => dictionaries.Select(FromDictionary).ToList();

    public PFRank(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; set; }  
    public string Name { get; set; }
}