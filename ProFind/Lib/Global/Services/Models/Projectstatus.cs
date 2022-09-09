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

        public override string ToString() => NamePs;
    }
}
