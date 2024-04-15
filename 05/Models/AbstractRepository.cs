
public class AbstractRepository<T> : IRepository<T> where T : Entity
{
    protected Dictionary<Guid, T> items = new Dictionary<Guid, T>();

    public IEnumerable<T> GetAll()
    {
       return items.Values;
    }

    public T GetSingle(Guid id)
    {
        if (items.ContainsKey(id))
            return items[id];

        return null;
    }

    public T Add(T entity)
    {
        entity.ID = Guid.NewGuid();
        items.Add(entity.ID, entity);
        return entity;
    }

    public T Update(T entity)
    {
        items[entity.ID] = entity;
        return entity;
    }

    public void Delete(Guid id)
    {
        items.Remove(id);
    }
}