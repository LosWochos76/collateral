
using System.Security.Cryptography;
using System.Text;

public class UserMemoryRepository : AbstractRepository<User>, IUserRepository
{
    private IConfiguration configuration;

    public UserMemoryRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public User FindByLogin(LoginData login)
    {
        var salt = configuration.GetValue<string>("Jwt:Key");
        var passwordHash = ComputeSha256Hash(login.Password, salt);
        var email = login.EMail.ToLower();

        foreach (var user in items.Values)
            if (user.EMail.ToLower().Equals(email) &&
                user.PasswordHash.Equals(passwordHash))
                return user;

        return null;
    }

    private static string ComputeSha256Hash(string rawData, string salt)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(rawData + salt);
            bytes = sha256Hash.ComputeHash(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}