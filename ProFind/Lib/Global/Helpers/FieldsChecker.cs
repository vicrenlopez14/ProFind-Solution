using System;
using System.Text.RegularExpressions;
using Windows.System;
using Windows.UI.Popups;

namespace ProFind.Lib.Global.Helpers
{
    public class FieldsChecker
    {
        public static bool CheckEmail(string email)
        {
            string expresion = "^[_a-z0-9-]+(.[_a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9]+)(.[a-z]{2,4})$";
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
        public static bool CheckDateUp(DateTimeOffset date)
        {
            return date >= DateTimeOffset.Now.Date;
        }

        public static bool CheckDateDown(DateTimeOffset date)
        {
            return date <= DateTimeOffset.Now.Date;
        }


        public static bool OnlyLetters(string text)
        {
            foreach (char character in text)
            {
                if (character >= '0' && character <= '9') return false;
            }
            return true;
        }

        public static bool CheckName(string text)
        {
            text = text.ToLower();
            Console.WriteLine(text);
            foreach (char character in text)
            {
                if ((character >= 'a' && character <= 'z') || character == ' ') continue;
                if (((character >= 'á' && character <= 'ú') || character == 'é')) continue;
                return false;
            }
            return true;
        }

        public static bool OnlyInts(string number)
        {
            //foreach (char character in number.ToString())
            //{
            //    if ((character >= '0' && character <= '9')) continue;
            //    return false;
            //}
            return true;
        }

        public static bool OnlyFloats(string number)
        {
            int count = 0;
            foreach (char character in number)
            {
                if (character == '.') count++;
                if ((character >= '0' && character <= '9') || (character == '.' && count < 2)) continue;
                return false;
            }
            return true;
        }

        public static bool CheckDUI(string dui)
        {
            if (dui.Length == 10)
            {
                int count = 0;
                foreach (char character in dui)
                {
                    if (character == '-') count++;
                    if ((character >= '0' && character <= '9') || (character == '-' && count < 2)) continue;
                    return false;
                }
                if (count == 1)
                {
                    if (dui[8] == '-') return true;
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        public static bool CheckAfp (string afp)
        {
            return afp.Length == 12;
        }

        public static bool CheckIsss (string isss)
        {
            return isss.Length == 9;
        }

        public static bool CheckPhoneNumber(string phone)
        {
            return phone.Length == 8;
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
