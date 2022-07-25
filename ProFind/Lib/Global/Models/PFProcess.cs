using System;

namespace ProFind.Lib.Global.Models
{
    public class PFProcess
    {
        public PFProcess(string id, string title, string description, PFTag pfTag, DateTime beginDate,
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
        public PFTag PfTag { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}