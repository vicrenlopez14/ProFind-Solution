namespace ProFind.Lib.Global.Services
{
    public partial class Proposal
    {
        public Proposal(string idPp, string titlePp, string descriptionPp, byte[] picturePp, DateOnly suggestedStart, DateOnly suggestedEnd, bool? seen, string revisionStatus, string idP3, string idC3)
        {
            IdPp = idPp;
            TitlePp = titlePp;
            DescriptionPp = descriptionPp;
            PicturePp = picturePp;
            SuggestedStart = suggestedStart;
            SuggestedEnd = suggestedEnd;
            Seen = seen;
            RevisionStatus = revisionStatus;
            IdP3 = idP3;
            IdC3 = idC3;
        }

        public override string ToString() => TitlePp;
    }
}
