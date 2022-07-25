using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProFind_WebService.Lib.Process.Model;

public class PFProcess
{
    [JsonConstructor]
    public PFProcess(string id, string? title = null, string? description = null, DateTime? beginDate = default,
        string? idTag = null, DateTime? endDate = default)
    {
        Id = id;
        Title = title;
        Description = description;
        BeginDate = beginDate;
        IdTag = idTag;
        EndDate = endDate;
    }


    [Column("Id_PR")] public string Id { get; set; }
    [Column("Title_PR")] public string? Title { get; set; }
    [Column("Description_PR")] public string? Description { get; set; }
    [Column("Begin_Date_PR")] public DateTime? BeginDate { get; set; }
    [Column("Id_T1")] public string? IdTag { get; set; }
    [Column("End_date_PR")] public DateTime? EndDate { get; set; }
}