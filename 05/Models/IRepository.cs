public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T GetSingle(Guid id);
    T Add(T entity);
    void Delete(Guid id);
    T Update(T entity);
}