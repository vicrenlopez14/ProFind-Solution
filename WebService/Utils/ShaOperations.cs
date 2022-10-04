using System.Security.Cryptography;
using System.Text;

namespace WebService.Utils;

public class ShaOperations
{
    private static string input1 = "";
    private static string input2 = "";

    public static string ShaPassword(string password)
    {
        if (String.IsNullOrEmpty(password))
            return String.Empty;

        using HashAlgorithm algorithm = SHA512.Create();
        var hashedString = Encoding.UTF8.GetString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(password)));

        if (input1 == null)
        {
            input1 = hashedString;
        }
        else
        {
            input2 = hashedString;
        }

        return hashedString;
    }

    public static string GenerateUID()
    {
        // UID by nanoid
        return Nanoid.Nanoid.Generate();
    }
}