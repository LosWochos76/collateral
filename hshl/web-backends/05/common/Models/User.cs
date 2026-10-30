using System.Security.Claims;

namespace Common.Models;

public record ExternalLogin(string Provider, string Subject);

public class User : Entity
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string EMail { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }  // external users have no password
    public bool IsAdmin { get; set; }
    public bool MailAddressConfirmed { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? PasswordResetTokenExpires { get; set; }
    public List<ExternalLogin> ExternalLogins { get; set; } = new List<ExternalLogin>();

    // standard claim types, also used by JWT, Google and Keycloak
    public List<Claim> ToClaims()
    {
        var claims = new List<Claim>
        {
            new Claim("sub", ID.ToString()),
            new Claim("email", EMail)
        };

        if (IsAdmin)
            claims.Add(new Claim("role", "Admin"));

        return claims;
    }
}
