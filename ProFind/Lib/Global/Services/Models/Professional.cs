namespace ProFind.Lib.Global.Services
{
    public partial class Professional
    {
        public Professional(string idP, string nameP, DateOnly dateBirthP, string emailP, string passwordP, bool? activeP, bool? sexP, string duip, string afpp, string isssp, string zipCodeP, float? salaryP, DateOnly hiringDateP, byte[] pictureP, byte[] curriculumP, double? latitudeLocationP, double? longitudeLocationP, int? idPfs1, int? idDp1, int? idWdt1)
        {
            IdP = idP;
            NameP = nameP;
            DateBirthP = dateBirthP;
            EmailP = emailP;
            PasswordP = passwordP;
            ActiveP = activeP;
            SexP = sexP;
            Duip = duip;
            Afpp = afpp;
            Isssp = isssp;
            ZipCodeP = zipCodeP;
            SalaryP = salaryP;
            HiringDateP = hiringDateP;
            PictureP = pictureP;
            CurriculumP = curriculumP;
            LatitudeLocationP = latitudeLocationP;
            LongitudeLocationP = longitudeLocationP;
            IdPfs1 = idPfs1;
            IdDp1 = idDp1;
            IdWdt1 = idWdt1;
        }

        public override string ToString() => NameP;
    }
}
