using Common.Misc;
using Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Persistence.Repositories;

public class UserDapperRepository : IUserRepository
{
    private ILogger<ToDoDapperRepository> logger;
    private GeneralSettings settings;

    public UserDapperRepository(ILogger<ToDoDapperRepository> logger, IOptions<GeneralSettings> settings)
    {
        this.logger = logger;
        this.settings = settings.Value;
    }

    public User Add(User entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public User FindByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public User FindByLogin(string email, string password)
    {
        throw new NotImplementedException();
    }

    public User FindByPasswordResetToken(string token)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public User GetSingle(Guid id)
    {
        throw new NotImplementedException();
    }

    public User Update(User entity)
    {
        throw new NotImplementedException();
    }
}