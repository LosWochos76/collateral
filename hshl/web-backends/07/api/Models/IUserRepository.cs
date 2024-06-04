namespace ToDoService.Models;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User GetSingle(Guid id);
    User Add(User entity);
    void Delete(Guid id);
    User Update(User entity);
    User FindByLogin(LoginData login);
}