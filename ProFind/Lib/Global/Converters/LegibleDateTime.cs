using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ProFind.Lib.Global.Converters
{
    public class LegibleDateTime : IValueConverter
    {
        // its a little bit tricky to inject this using IoC, but it's possible

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var type = value.GetType();

            if (!(type == typeof(DateTimeOffset))) return "Unknown";

            var dateToParse = value as DateTimeOffset?;

            return dateToParse.Value.ToString("dd MMM yyyy");

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return 0;
        }
    }
}
