namespace ProFind.Lib.Global.Services
{
    public partial class Tag
    {
        public Tag(string idT, string nameT, byte[] colorT, string idPj1)
        {
            IdT = idT;
            NameT = nameT;
            ColorT = colorT;
            IdPj1 = idPj1;
        }

        public override string ToString() => NameT;
    }
}
