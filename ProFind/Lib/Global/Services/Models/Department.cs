namespace ProFind.Lib.Global.Services

{
    public partial class Department
    {
        public Department(int? idDp, string nameDp)
        {
            IdDp = idDp;
            NameDp = nameDp;
        }

        public override string ToString() => NameDp;
    }
}
