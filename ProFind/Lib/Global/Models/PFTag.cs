namespace ProFind.Lib.Global.Models
{
    public class PFTag
    {
        public PFTag(string id, string name, PFProject pfProject)
        {
            Id = id;
            Name = name;
            PfProject = pfProject;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public PFProject PfProject { get; set; }
    }
}