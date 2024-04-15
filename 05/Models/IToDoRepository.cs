public interface IToDoRepository : IRepository<ToDo>
{
    IEnumerable<ToDo> GetAllForUser(User user);
}