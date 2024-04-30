namespace ToDoService.Models;

public interface IToDoRepository
{
    ToDoListResult GetAll(ToDoFilter filter);
    ToDo GetSingle(Guid id);
    ToDo Add(ToDo entity);
    void Delete(Guid id);
    ToDo Update(ToDo entity);
    ToDoListResult GetAllForUser(User user, ToDoFilter filter);
}