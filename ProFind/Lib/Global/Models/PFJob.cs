namespace ProFind.Lib.Global.Models
{
    public class PFJob
    {
        public PFJob(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}