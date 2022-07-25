namespace ProFind.Lib.Global.Models
{
    public class PFClient
    {
        public PFClient(string id, string name, string email, string creditcard)
        {
            Id = id;
            Name = name;
            Email = email;
            CreditCard = creditcard;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CreditCard { get; set; }
    }
}