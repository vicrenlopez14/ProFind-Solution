using Application.Interfaces;

namespace ProFind_WebService.Lib.Process.Model;

public class PFProcess : DedictionarizableEntity<PFProcess>
{
    private DedictionarizableEntity<PFProcess> _dedictionarizableEntityImplementation;

    public static PFProcess FromDictionary(IDictionary<string, object> dictionary)
    {
        return new PFProcess(dictionary["Id_P"].ToString(), dictionary["Title_P"].ToString(),
            dictionary["Description_P"].ToString(),
            Lib.Tag.Model.PFTag.FromDictionary(dictionary), DateTime.Parse(dictionary["Begin_Date_P"].ToString()),
            DateTime.Parse(dictionary["End_Date_P"].ToString()));
    }

    public static IEnumerable<PFProcess> FromDictionary(IEnumerable<IDictionary<string, object>> dictionaries)
        => dictionaries.Select(FromDictionary).ToList();

    public PFProcess(string id, string title, string description, Tag.Model.PFTag pfTag, DateTime beginDate,
        DateTime endDate)
    {
        Id = id;
        Title = title;
        Description = description;
        this.PfTag = pfTag;
        BeginDate = beginDate;
        EndDate = endDate;
    }

    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Tag.Model.PFTag PfTag { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
}