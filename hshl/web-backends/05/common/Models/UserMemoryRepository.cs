using Microsoft.AspNetCore.Identity;

namespace Common.Models;

public class UserMemoryRepository : IUserRepository
{
    private IPasswordHasher<User> passwordHasher;
    private Dictionary<Guid, User> items = new Dictionary<Guid, User>();

    public UserMemoryRepository(IPasswordHasher<User> passwordHasher)
    {
        this.passwordHasher = passwordHasher;
        AddWithPassword(new User() { EMail="alexander.stuckenholz@hshl.de", IsAdmin=true }, "secret");
        AddWithPassword(new User() { EMail="student@hshl.de", IsAdmin=false }, "secret");
    }

    private void AddWithPassword(User user, string password)
    {
        user.PasswordHash = passwordHasher.HashPassword(user, password);
        Add(user);
    }

    public IEnumerable<User> GetAll()
    {
       return items.Values;
    }

    public User? GetSingle(Guid id)
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

    public User? FindByEmail(string email)
    {
        foreach (var user in items.Values)
            if (string.Equals(user.EMail, email, StringComparison.OrdinalIgnoreCase))
                return user;
        
        return null;
    }

    public User? FindByLogin(string email, string password)
    {
        var user = FindByEmail(email);
        if (user is null || user.PasswordHash is null)
            return null;

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        if (result == PasswordVerificationResult.Failed)
            return null;

        if (result == PasswordVerificationResult.SuccessRehashNeeded)
            user.PasswordHash = passwordHasher.HashPassword(user, password);

        return user;
    }

    public User? FindByPasswordResetToken(string token)
    {
        foreach (var user in items.Values)
            if (user.PasswordResetToken == token && user.PasswordResetTokenExpires > DateTime.UtcNow)
                return user;
        
        return null;
    }

    public User? FindByExternalLogin(string provider, string subject)
    {
        foreach (var user in items.Values)
            if (user.ExternalLogins.Contains(new ExternalLogin(provider, subject)))
                return user;

        return null;
    }

    public User AddExternalUser(string provider, string subject, string? email)
    {
        // no automatic linking via e-mail: not every provider guarantees a verified address
        var user = new User() { EMail = email ?? string.Empty };
        user.ExternalLogins.Add(new ExternalLogin(provider, subject));
        return Add(user);
    }
}
