namespace ProFind.Lib.Global.Services
{
    public partial class Profession
    {
        public Profession(int? idPfs, string namePfs)
        {
            IdPfs = idPfs;
            NamePfs = namePfs;
        }

        public override string ToString() => NamePfs;
    }
}
