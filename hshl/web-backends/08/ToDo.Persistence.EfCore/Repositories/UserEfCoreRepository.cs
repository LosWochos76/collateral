using ToDoManager.Common.Misc;
using ToDoManager.Common.Models;

namespace ToDoManager.Persistence.EfCore;

public class UserEfCoreRepository : IUserRepository
{
    private readonly ApplicationDbContext context;
    private PasswordHelper passwordHelper;

    public UserEfCoreRepository(ApplicationDbContext context, PasswordHelper passwordHelper)
    {
        this.context = context;
        this.passwordHelper = passwordHelper;
    }

    public IEnumerable<User> GetAll()
    {
        return context.Users.ToList();
    }

    public User GetSingle(Guid id)
    {
        return context.Users.Find(id);
    }

    public User Add(User entity)
    {
        context.Users.Add(entity);
        context.SaveChanges();
        return entity;
    }

    public void Delete(Guid id)
    {
        var entity = context.Users.Find(id);
        if (entity != null)
        {
            context.Users.Remove(entity);
            context.SaveChanges();
        }
    }

    public User Update(User entity)
    {
        context.Users.Update(entity);
        context.SaveChanges();
        return entity;
    }

    public User FindByEmail(string email)
    {
        return context.Users.FirstOrDefault(u => u.EMail.ToLower().Equals(email.ToLower()));
    }

    public User FindByLogin(string email, string password)
    {
        var passwordHash = passwordHelper.ComputeSha256Hash(password);
        return context.Users.FirstOrDefault(u => u.EMail.ToLower().Equals(email.ToLower())
            && u.PasswordHash.Equals(passwordHash));
    }

    public User FindByPasswordResetToken(string token)
    {
        return context.Users.FirstOrDefault(u => u.PasswordResetToken == token);
    }
}

