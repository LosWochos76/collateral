public class ToDoMemoryRepository : AbstractRepository<ToDo>, IToDoRepository
{
    public IEnumerable<ToDo> GetAllForUser(User user)
    {
        var result = new List<ToDo>();
        foreach (var item in items.Values)
            if (item.Owner.ID.Equals(user.ID))
                result.Add(item);

        return result;
    }
}