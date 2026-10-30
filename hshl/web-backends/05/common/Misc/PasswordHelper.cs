using System.Security.Cryptography;

namespace Common.Misc;

public class PasswordHelper
{
    // cryptographically secure random token, e.g. for password reset links
    public string GenerateToken()
    {
        return RandomNumberGenerator.GetHexString(64);
    }
}
