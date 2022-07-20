using Application.Interfaces;

namespace ProFind_WebService.Lib.Curriculum.Model;

public class PFCurriculum : DedictionarizableEntity<PFCurriculum>
{
    private DedictionarizableEntity<PFCurriculum> _dedictionarizableEntityImplementation;

    public static PFCurriculum FromDictionary(IDictionary<string,object> dictionary)
    {
        return new PFCurriculum(dictionary["Id_CU"].ToString(), dictionary["Studies_CU"].ToString(),
            dictionary["Experiences_CU"].ToString(), int.Parse(dictionary["Years_CU"].ToString()));
    }

    public static IEnumerable<PFCurriculum> FromDictionary(IEnumerable<IDictionary<string,object>> dictionaries)
        => dictionaries.Select(FromDictionary).ToList();

    public PFCurriculum(string id, string studies, string experiences, int years)
    {
        Id = id;
        Studies = studies;
        Experiences = experiences;
        Years = years;
    }

    public string Id { get; set; }
    public string Studies { get; set; }
    public string Experiences { get; set; }
    public int Years { get; set; }  

}
