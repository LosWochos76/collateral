using ToDoManager.Common.Models;

namespace ToDoManager.Persistence;

public interface IToDoRepository
{
    PagedResult<ToDo> GetAll(ToDoFilter filter);
    ToDo GetSingle(Guid id);
    ToDo Add(ToDo entity);
    void Delete(Guid id);
    ToDo Update(ToDo entity);
}