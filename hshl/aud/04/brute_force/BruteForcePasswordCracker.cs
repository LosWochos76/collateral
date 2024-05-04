using System;
using System.Security.Cryptography;
using System.Text;

public class BruteForcePasswordCracker
{
    static string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    static int attempts = 0;

    private static string ComputeSha256Hash(string rawData)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
                builder.Append(bytes[i].ToString("x2"));

            return builder.ToString();
        }
    }

    private static bool IsEqual(string password, string hash)
    {
        return ComputeSha256Hash(password) == hash;
    }

    // Als test sei hash = "095fffbf92f0fcff322081a2c3bd09b56f91b8bc7cda7b73d23d86d102f7cf27"
    public static string CrackPassword(string hash)
    {
        string attempt = "";

        do
        {
            attempt = NextCandidateLinear(attempt);
            Console.WriteLine(attempt);
            attempts++;
        }
        while (!IsEqual(attempt, hash));

        Console.WriteLine($"Passwort gefunden: {attempt}");
        Console.WriteLine($"Versuche benötigt: {attempts}");
        return attempt;
    }

    private static char NextChar(char c)
    {
        return characters[(characters.IndexOf(c) + 1) % characters.Length];
    }

    private static string NextCandidateRecursive(string current, int pos)
    {
        if (pos > current.Length - 1)
            return current + characters[0];

        var chars = current.ToCharArray();
        var next_char = NextChar(chars[pos]);
        chars[pos] = next_char;
        current = new string(chars);

        if (next_char == 'a')
            current = NextCandidateRecursive(current, pos + 1);

        return current; 
    }

    public static string NextCandidateLinear(string passw)
    {
        //aktuelles Passwort von rechts nach links durchgehen:
        for(int i = passw.Length - 1; i >= 0; i--)
        {
            if (passw[i] == characters[characters.Length - 1]) // wenn letzter in characters: zurrücksetzten 
            {
                passw = passw.Remove(i, 1).Insert(i, characters[0].ToString());
            }
            else // wenn nicht, einfach durch nächsten in characters austauschen 
            {
                int PosChar = characters.IndexOf(passw[i]);
                return passw.Remove(i, 1).Insert(i, characters[PosChar + 1].ToString());
            }
        }

        // alle Kombinationen für die Passwortlänge durch -> weitere stelle hinzufügen
        return passw + characters[0];
    }
}