using Application.Interfaces;

namespace ProFind_WebService.Lib.Job.Model;

public class PFJob : DedictionarizableEntity<PFJob>
{
    private DedictionarizableEntity<PFJob> _dedictionarizableEntityImplementation;

    public static PFJob FromDictionary(IDictionary <string,object> dictionary)
    {
        return new PFJob(dictionary["Id_J"].ToString(), dictionary["Name_J"].ToString());
    }

    public static IEnumerable<PFJob> FromDictionary(IEnumerable<IDictionary<string,object>> dictionaries)
        => dictionaries.Select(FromDictionary).ToList();    

    public PFJob(string id, string name)
    {
        Id=id;
        Name=name;  
    }

    public string Id { get; set; }  
    public string Name { get; set; }    
}