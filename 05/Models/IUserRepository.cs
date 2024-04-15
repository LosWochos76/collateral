public interface IUserRepository : IRepository<User>
{
    User FindByLogin(LoginData login);
}