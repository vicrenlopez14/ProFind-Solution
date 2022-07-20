using Application.Interfaces;

namespace ProFind_WebService.Lib.Project.Model;

public class PFProject : DedictionarizableEntity<PFProject>
{
    public PFProject(string id, string title, string description)
    {
        Id = id;
        Title = title;
        Description = description;
    }

    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public static PFProject FromDictionary(IDictionary<string, object> dictionary)
    {
        return new PFProject(dictionary["Id_PJ"].ToString(), dictionary["Title_PJ"].ToString(),
            dictionary["Description_PJ"].ToString());
    }

    public static IEnumerable<PFProject> FromDictionary(IEnumerable<IDictionary<string, object>> dictionaries)
        => dictionaries.Select(FromDictionary).ToList();
}