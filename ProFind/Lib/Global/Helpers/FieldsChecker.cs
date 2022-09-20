using System;
using System.Text.RegularExpressions;
using Windows.System;

namespace ProFind.Lib.Global.Helpers
{
    public class FieldsChecker
    {
        public static bool CheckEmail(string email)
        {
            string expresion = "^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9]+)*(.[a-z]{2,4})$";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool CheckRangeDate(DateTimeOffset begin, DateTimeOffset end)
        {
            return begin >= DateTimeOffset.Now && end >= begin;
        }

        public static bool CheckDateUp (DateTimeOffset date)
        {
            return date >= DateTimeOffset.Now;
        }

        public static bool CheckDateDown (DateTimeOffset date)
        {
            return date <= DateTimeOffset.Now;
        }

        public static bool OnlyLetters(Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if ((e.Key >= VirtualKey.A && e.Key <= VirtualKey.Z) || (e.Key == VirtualKey.Back 
                || e.Key == VirtualKey.Space)) return true;
            else return false;
        }

        public static bool OnlyInts(Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if ((e.Key >= VirtualKey.Number0 && e.Key <= VirtualKey.Number9) || (e.Key == VirtualKey.Back)) return true;
            else return false;
        }

        public static bool OnlyLFloats(Windows.UI.Xaml.Input.KeyRoutedEventArgs e, string number)
        {
            if ((e.Key >= VirtualKey.Number0 && e.Key <= VirtualKey.Number9) || (e.Key == VirtualKey.Back)) return true;
            else if (number.Length > 0 && e.Key == VirtualKey.Decimal && !number.Contains(".")) return true;
            else return false;
        }

        public static bool CheckPassword(string pass)
        {
            return (pass.Length >= 5);
        }
        public static bool CheckPassword(string pass, string confirm)
        {
            return (pass.Length >= 5 && pass == confirm);
        }
    }

}
