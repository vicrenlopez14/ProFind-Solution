namespace ProFind.Lib.Global.Models
{
    public class PFAdmin
    {
        public PFAdmin(string id)
        {
            Id = id;
        }

        public PFAdmin(string id, string name, string email, string tel, PFRank rank)
        {
            Id = id;
            Name = name;
            Email = email;
            Tel = tel;
            Rank = rank;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public PFRank Rank { get; set; }
    }
}