namespace ProFind.Lib.Global.Services
{
    public partial class Activity
    {
        public Activity(string idA, string titleA, string descriptionA, DateOnly expectedBeginA, DateOnly expectedEndA, byte[] pictureA, string idPj1, string idT1)
        {
            IdA = idA;
            TitleA = titleA;
            DescriptionA = descriptionA;
            ExpectedBeginA = expectedBeginA;
            ExpectedEndA = expectedEndA;
            PictureA = pictureA;
            IdPj1 = idPj1;
            IdT1 = idT1;
        }

        public override string ToString() => TitleA;
    }
}
