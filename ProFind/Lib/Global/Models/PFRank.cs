namespace ProFind.Lib.Global.Models
{
    public class PFRank
    {
        public PFRank(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}