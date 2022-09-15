namespace ProFind.Lib.Global.Services
{
    public partial class Admin
    {
        public Admin()
        {
            
        }
        public Admin(string idA, string nameA, string emailA, string telA, string passwordA, byte[] pictureA, string idR1)
        {
            IdA = idA;
            NameA = nameA;
            EmailA = emailA;
            TelA = telA;
            PasswordA = passwordA;
            PictureA = pictureA;
            IdR1 = idR1;
        }

        public override string ToString() => NameA;
    }
}
