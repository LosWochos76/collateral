using System;
using System.Security.Cryptography;
using System.Text;

public class BruteForcePasswordCracker
{
    static string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    static string currentAttempt = string.Empty;
    static int attempts = 0;

    static string ComputeSha256Hash(string rawData)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }

    public static bool Login(string password)
    {
        return ComputeSha256Hash(password) == "095fffbf92f0fcff322081a2c3bd09b56f91b8bc7cda7b73d23d86d102f7cf27";
    }

    public static void Main(string[] args)
    {
        do
        {
            currentAttempt = NextCandidate();
            attempts++;
        }
        while (!Login(currentAttempt));

        Console.WriteLine($"Passwort gefunden: {currentAttempt}");
        Console.WriteLine($"Versuche benötigt: {attempts}");
    }

    public static string NextCandidate()
    {
        // Hier fehlt etwas
    }
}
