using ProFind.Lib.Global.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace ProFind.Lib.Global.Converters
{
    public class TagDurationReadable : IValueConverter
    {
        // its a little bit tricky to inject this using IoC, but it's possible

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is int)) return "Unknown";

            switch (value)
            {
                case 0:
                    return "Short";
                case 1:
                    return "Medium";
                case 2:
                    return "Long";
                default:
                    return "Unknown";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return 0;
        }
    }
}