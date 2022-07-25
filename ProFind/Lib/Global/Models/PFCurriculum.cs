namespace ProFind.Lib.Global.Models
{
    public class PFCurriculum
    {
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
}