namespace ProFind.Lib.Global.Models
{
    public class PFProject
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
    }
}