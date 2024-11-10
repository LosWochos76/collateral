using ToDoManager.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoManager.Persistence.EfCore;

public class ToDoEfCoreRepository : IToDoRepository
{
    private readonly ApplicationDbContext context;

    public ToDoEfCoreRepository(ApplicationDbContext context)
    {
        this.context = context;
        context.Migrate();
    }

    public PagedResult<ToDo> GetAll(ToDoFilter filter)
    {
        var query = context.ToDos.AsQueryable();

        var totalCount = query.Count();
        IEnumerable<ToDo> result;

        if (filter.StartPage >= 0)
            result = query.AsNoTracking()
                .Skip(filter.StartPage * filter.ItemsPerPage)
                .Take(filter.ItemsPerPage)
                .ToList();
        else
            result = query.AsNoTracking().ToList();

        return new PagedResult<ToDo>
        {
            TotalItems = totalCount,
            CurrentPage = filter.StartPage,
            TotalPages = totalCount / filter.ItemsPerPage + 1,
            Items = result
        };
    }

    public ToDo GetSingle(Guid id)
    {
        return context.ToDos.AsNoTracking().Where(x => x.ID == id).FirstOrDefault();
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
        context.Entry(entity).State = EntityState.Detached;
        return entity;
    }
}