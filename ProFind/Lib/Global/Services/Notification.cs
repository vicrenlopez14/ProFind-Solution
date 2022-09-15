using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFind.Lib.Global.Services
{
    public partial class Notification
    {
        public Notification()
        {
        }

        public Notification(string idN, string titleN, string descriptionN, DateOnly dateN, byte[] pictureN)
        {
            IdN = idN;
            TitleN = titleN;
            DescriptionN = descriptionN;
            DateTimeIssuedN = dateN;
            PictureN = pictureN;
        }

        public override string ToString() => TitleN;
    }
}
