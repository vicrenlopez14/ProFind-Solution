using Application.Interfaces;

namespace ProFind_WebService.Lib.Tag.Model;

public class PFTag : DedictionarizableEntity<PFTag>
{
    public static PFTag FromDictionary(IDictionary<string, object> dictionary)
    {
        return new PFTag(dictionary["Id_T"].ToString(), dictionary["Name_T"].ToString(),
            Lib.Project.Model.PFProject.FromDictionary(dictionary));
    }

    public static IEnumerable<PFTag> FromDictionary(IEnumerable<IDictionary<string, object>> dictionaries)
        => dictionaries.Select(FromDictionary).ToList();


    public PFTag(string id, string name, Project.Model.PFProject pfProject)
    {
        Id = id;
        Name = name;
        PfProject = pfProject;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public Project.Model.PFProject PfProject { get; set; }
}