using System.Text.RegularExpressions;

namespace WebService.Utils;

public class DataIntegrityValidations
{
    // Check a text doesnt have numbers
    public static bool CheckText(string text)
    {
        return !text.Any(char.IsDigit);
    }

    // Check that a date is not in the past
    public static bool CheckDate(DateTime date)
    {
        return date > DateTime.Now;
    }

    // Check that a date is equal or greater than another date
    public static bool CheckDate(DateTime date1, DateTime date2)
    {
        return date1 >= date2;
    }

    // Check email format
    public static bool CheckEmail(string email)
    {
        return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
    }
}