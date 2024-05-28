public class BruteForcePasswordCracker
{
    static string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    private static bool AreHashedValuesEqual(string password, string hash)
    {
        return HashHelper.ComputeSha256Hash(password) == hash;
    }

    // Als test sei hash = "095fffbf92f0fcff322081a2c3bd09b56f91b8bc7cda7b73d23d86d102f7cf27" gegeben
    public static string CrackPassword(string hash)
    {
        string attempt = "";
        int attempts = 0;

        do
        {
            attempt = GetCandidate(attempts);
            Console.WriteLine(attempt);
            attempts++;
        }
        while (!AreHashedValuesEqual(attempt, hash));

        Console.WriteLine($"Passwort gefunden: {attempt}");
        Console.WriteLine($"Versuche benötigt: {attempts}");
        return attempt;
    }

    private static string GetCandidate(int attempt)
    {
        var candidate = string.Empty;
        
        // Hier fehlt Ihr Programmcode, um aus der Zahl in "attempt" ein Passwort zu machen.

        return candidate;
    }
}