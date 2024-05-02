using System.Security.Cryptography;
using System.Text;

namespace ToDoService.Models;

public class UserMemoryRepository : IUserRepository
{
    private IConfiguration configuration;
    private Dictionary<Guid, User> items = new Dictionary<Guid, User>();

    public UserMemoryRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public IEnumerable<User> GetAll()
    {
       return items.Values;
    }

    public User GetSingle(Guid id)
    {
        if (items.ContainsKey(id))
            return items[id];

        return null;
    }

    public User Add(User entity)
    {
        entity.ID = Guid.NewGuid();
        items.Add(entity.ID, entity);
        return entity;
    }

    public User Update(User entity)
    {
        items[entity.ID] = entity;
        return entity;
    }

    public void Delete(Guid id)
    {
        items.Remove(id);
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