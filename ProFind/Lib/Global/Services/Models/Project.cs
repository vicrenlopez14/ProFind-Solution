namespace ProFind.Lib.Global.Services
{
    public partial class Project
    {
        public Project(string idPj, string titlePj, string descriptionPj, byte[] picturePj, float? totalPricePj, int? idPs1, string idP1, string idC1)
        {
            IdPj = idPj;
            TitlePj = titlePj;
            DescriptionPj = descriptionPj;
            PicturePj = picturePj;
            TotalPricePj = totalPricePj;
            IdPs1 = idPs1;
            IdP1 = idP1;
            IdC1 = idC1;
        }

        public override string ToString() => TitlePj;
    }
}
