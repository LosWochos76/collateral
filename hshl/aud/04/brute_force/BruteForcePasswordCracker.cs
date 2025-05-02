public class BruteForcePasswordCracker
{
    static string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public static bool AreHashedValuesEqual(string password, string hash)
    {
        return HashHelper.ComputeSha256Hash(password) == hash;
    }
    
    public static bool IsPasswordCorrect(string password)
    {
        return AreHashedValuesEqual(password, "095fffbf92f0fcff322081a2c3bd09b56f91b8bc7cda7b73d23d86d102f7cf27");
    }
}