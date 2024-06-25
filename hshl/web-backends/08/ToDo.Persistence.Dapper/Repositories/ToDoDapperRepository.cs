using Dapper;
using DapperQueryBuilder;
using Microsoft.Extensions.Logging;
using ToDoManager.Common.Models;

namespace ToDoManager.Persistence.Dapper.Repositories;

public class ToDoDapperRepository : IToDoRepository
{
    private ILogger<ToDoDapperRepository> logger;
    private IDbConnectionFactory factory;

    public ToDoDapperRepository(ILogger<ToDoDapperRepository> logger, IDbConnectionFactory factory)
    {
        this.logger = logger;
        this.factory = factory;

        //CreateTableIfNotExists();
    }

    public ToDo Add(ToDo entity)
    {
        entity.ID = Guid.NewGuid();
        using (var connection = factory.GetConnection())
        {
            var sql = "INSERT INTO ToDos (ID, Title, Completion, Description) VALUES (@ID, @Title, @Completion, @Description) RETURNING *";
            return connection.QuerySingle<ToDo>(sql, entity);
        }
    }

    public void Delete(Guid id)
    {
        using (var connection = factory.GetConnection())
        {
            var sql = "DELETE FROM ToDos WHERE ID = @Id";
            connection.Execute(sql, new { Id = id });
        }
    }

    public PagedResult<ToDo> GetAll(ToDoFilter filter)
    {
        using (var connection = factory.GetConnection())
        {
            var sql = "SELECT * FROM ToDos WHERE 1=1";
            var countSql = "SELECT COUNT(*) FROM ToDos WHERE 1=1";
            var parameters = new DynamicParameters();

            // Dynamische Filterausdrücke hinzufügen
            foreach (var expression in filter.FilterExpressions)
            {
                var clause = GetFilterClause(expression, parameters);
                sql += clause;
                countSql += clause;
            }
            
            var totalItems = connection.ExecuteScalar<int>(countSql, parameters);

            // OrderBy-Klausel hinzufügen
            if (!string.IsNullOrEmpty(filter.OrderBy))
                sql += " ORDER BY " + filter.OrderBy;

            // Paginierung hinzufügen
            if (filter.StartPage >= 0 && filter.ItemsPerPage > 0)
            {
                sql += " OFFSET @Offset LIMIT @Limit";
                parameters.Add("Offset", filter.StartPage * filter.ItemsPerPage);
                parameters.Add("Limit", filter.ItemsPerPage);
            }

            var items = connection.Query<ToDo>(sql, parameters).ToList();
            var totalPages = (int)Math.Ceiling((double)totalItems / filter.ItemsPerPage);

            return new PagedResult<ToDo>
            {
                Items = items,
                CurrentPage = filter.StartPage,
                TotalPages = totalPages,
                TotalItems = totalItems
            };
        }
    }

    private string GetFilterClause(FilterExpression expression, DynamicParameters parameters)
    {
        var clause = string.Empty;
        var parameterName = $"@{expression.PropertyName}_{parameters.ParameterNames.Count()}";

        switch (expression.Relation)
        {
            case RelationType.Equal:
                clause = $" AND {expression.PropertyName} = {parameterName}";
                break;
            case RelationType.NotEqual:
                clause = $" AND {expression.PropertyName} != {parameterName}";
                break;
            case RelationType.Larger:
                clause = $" AND {expression.PropertyName} > {parameterName}";
                break;
            case RelationType.LargerOrEqual:
                clause = $" AND {expression.PropertyName} >= {parameterName}";
                break;
            case RelationType.Smaller:
                clause = $" AND {expression.PropertyName} < {parameterName}";
                break;
            case RelationType.SmallerOrEqual:
                clause = $" AND {expression.PropertyName} <= {parameterName}";
                break;
        }

        parameters.Add(parameterName, expression.Value);
        return clause;
    }

    public ToDo GetSingle(Guid id)
    {
        using (var connection = factory.GetConnection())
        {
            var sql = "SELECT * FROM ToDos WHERE ID = @Id";
            return connection.QuerySingleOrDefault<ToDo>(sql, new { Id = id });
        }
    }

    public ToDo Update(ToDo entity)
    {
        using (var connection = factory.GetConnection())
        {
            var sql = "UPDATE ToDos SET Title = @Title, Completion = @Completion, Description = @Description WHERE ID = @ID RETURNING *";
            return connection.QuerySingle<ToDo>(sql, entity);
        }
    }

    private void CreateTableIfNotExists()
    {
        var sql = $@"create table IF NOT EXISTS todos (
            id uuid primary key,
            title varchar(100) not null,
            completion int not null,
            description text
        );";

        using (var connection = factory.GetConnection())
        {
            connection.Execute(sql);
        }
    }
}