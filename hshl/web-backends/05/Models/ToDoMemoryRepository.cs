namespace ToDoService.Models;

public class ToDoMemoryRepository : IToDoRepository
{
    protected Dictionary<Guid, ToDo> items = new Dictionary<Guid, ToDo>();

    private IEnumerable<ToDo> Filter(IEnumerable<ToDo> input, ToDoFilter filter)
    {
        if (filter is null || filter.FilterExpressions is null || filter.FilterExpressions.Count() == 0)
            return input;

        var result = new List<ToDo>();
        foreach (var item in input)
        {
            bool applies = true;
            foreach (var expr in filter.FilterExpressions)
                applies = applies && CompliesFilter(item, expr);

            if (applies)
                result.Add(item);
        }

        return result;
    }

    private IEnumerable<ToDo> Order(IEnumerable<ToDo> input, ToDoFilter filter)
    {
        if (filter is null || string.IsNullOrEmpty(filter.OrderBy) || input.Count() == 0)
            return input;
        
        var prop = input.First().GetType().GetProperty(filter.OrderBy);
        return input.OrderBy(x => prop.GetValue(x, null));
    }

    private ToDoListResult Paginate(IEnumerable<ToDo> input, ToDoFilter filter)
    {
        var result = new ToDoListResult();

        if (filter is null || filter.StartPage == -1)
        {
            result.Items = input;
            result.Page = 0;
            return result;
        }

        result.PageCount = input.Count() / filter.ItemsPerPage;
        result.Items = input.Skip(filter.StartPage * filter.ItemsPerPage).Take(filter.ItemsPerPage);
        return result;
    }

    public ToDoListResult GetAll(ToDoFilter filter)
    {
        var objects = Filter(items.Values, filter);
        objects = Order(objects, filter);
        var result = Paginate(objects, filter);
        return result;
    }

    public ToDoListResult GetAllForUser(User user, ToDoFilter filter)
    {
        var objects = Filter(items.Values, filter);

        var objects_with_owner = new List<ToDo>();
        foreach (var item in objects)
            if (item.Owner.ID.Equals(user.ID))
                objects_with_owner.Add(item);

        objects = Order(objects_with_owner, filter);
        return Paginate(objects, filter);
    }

    private bool CompliesFilter(Object obj, FilterExpression expression)
    {
        var prop = obj.GetType().GetProperty(expression.PropertyName);
        dynamic value = prop.GetValue(obj, null);
        dynamic comparerValue = Convert.ChangeType(expression.Value, prop.PropertyType);

        switch (expression.Relation)
        {
            case RelationType.Equal: return value == comparerValue;
            case RelationType.NotEqual: return value != comparerValue;
            case RelationType.Larger: return value > comparerValue;
            case RelationType.LargerOrEqual: return value >= comparerValue;
            case RelationType.Smaller: return value < comparerValue;
            case RelationType.SmallerOrEqual: return value <= comparerValue;
        }

        return false;
    }

    private IEnumerable<object> OrderBy(IEnumerable<object> list, string properytName)
    {
        var prop = list.First().GetType().GetProperty(properytName);
        return list.OrderBy(x => prop.GetValue(x, null));
    }

    public ToDo GetSingle(Guid id)
    {
        if (items.ContainsKey(id))
            return items[id];

        return null;
    }

    public ToDo Add(ToDo entity)
    {
        entity.ID = Guid.NewGuid();
        items.Add(entity.ID, entity);
        return entity;
    }

    public ToDo Update(ToDo entity)
    {
        items[entity.ID] = entity;
        return entity;
    }

    public void Delete(Guid id)
    {
        items.Remove(id);
    }
}