using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace ProFind.Lib.Global.Converters
{
    public abstract class AsyncConverter<T> : IValueConverter
    {
        public abstract Task<T> AsyncConvert(object value, Type targetType, object parameter, string culture);

        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var res = new GenericAsyncViewModel();
            res.Start(AsyncConvert(value, targetType, parameter, culture));
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return null;
        }

        private class GenericAsyncViewModel : INotifyPropertyChanged
        {
            private T _result;
            public event PropertyChangedEventHandler PropertyChanged;

            public T Result
            {
                get => _result;
                set
                {
                    _result = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
                }
            }

            public async void Start(Task<T> task)
            {
                Result = await task;
            }

        }
    }
}