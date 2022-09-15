namespace ProFind.Lib.Global.Services
{
    public partial class Rank
    {
        public Rank(int? idR, string nameR)
        {
            IdR = idR;
            NameR = nameR;
        }

        public override string ToString() => NameR;
    }
}

