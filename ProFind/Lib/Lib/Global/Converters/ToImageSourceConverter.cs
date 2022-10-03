using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Nito.AsyncEx.Synchronous;
using ProFind.Lib.Global.Helpers;
using Nito.AsyncEx;

namespace ProFind.Lib.Global.Converters
{
    public class ToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is string base64String)) return null;

            try
            {
                return base64String.FromBase64StringSync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}