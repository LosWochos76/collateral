using ToDoManager.Common.Models;

namespace ToDoManager.Persistence.EfCore;

public class ToDoEfCoreRepository : IToDoRepository
{
    private readonly ApplicationDbContext context;

    public ToDoEfCoreRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public PagedResult<ToDo> GetAll(ToDoFilter filter)
    {
        var query = context.ToDos.AsQueryable();

        var totalCount = query.Count();
        var results = query
            .Skip((filter.StartPage - 1) * filter.ItemsPerPage)
            .Take(filter.ItemsPerPage)
            .ToList();

        return new PagedResult<ToDo>
        {
            TotalItems = totalCount,
            CurrentPage = filter.StartPage,
            TotalPages = totalCount / filter.ItemsPerPage + 1,
            Items = results
        };
    }

    public ToDo GetSingle(Guid id)
    {
        return context.ToDos.Find(id);
    }

    public ToDo Add(ToDo entity)
    {
        context.ToDos.Add(entity);
        context.SaveChanges();
        return entity;
    }

    public void Delete(Guid id)
    {
        var entity = context.ToDos.Find(id);
        if (entity != null)
        {
            context.ToDos.Remove(entity);
            context.SaveChanges();
        }
    }

    public ToDo Update(ToDo entity)
    {
        context.ToDos.Update(entity);
        context.SaveChanges();
        return entity;
    }
}

