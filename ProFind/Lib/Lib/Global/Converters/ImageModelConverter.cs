using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using ProFind.Lib.Global.Helpers;

namespace ProFind.Lib.Global.Converters
{
    public class ImageModelConverter : AsyncConverter<ImageSource>
    {
        // its a little bit tricky to inject this using IoC, but it's possible
        // but that's not part of this ... so I just create the service here
        public override async Task<ImageSource> AsyncConvert(object value, Type targetType, object parameter,
            string culture)
        {
            if (!(value is string base64String)) return null;

            try
            {
                return await base64String.FromBase64String();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}