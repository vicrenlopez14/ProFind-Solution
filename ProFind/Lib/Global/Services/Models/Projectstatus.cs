namespace ProFind.Lib.Global.Services
{
    public partial class Projectstatus
    {
        private int v;
        private object text;

        public Projectstatus()
        {
        }

        public Projectstatus(int v, object text)
        {
            this.v = v;
            this.text = text;
        }

        public Projectstatus(string idPs, string namePs, string descriptionPs, string colorPs, string idPj3, int v, object text)
        {
            IdPs = idPs;
            NamePs = namePs;
            DescriptionPs = descriptionPs;
            ColorPs = colorPs;
            IdPj3 = idPj3;
            this.v = v;
            this.text = text;
        }

        public override string ToString() => NamePs;
    }
}
