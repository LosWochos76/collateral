using Common.Models;
using Dapper;
using DapperQueryBuilder;
using Microsoft.Extensions.Logging;
using Persistence.Misc;

namespace Persistence.Repositories;

public class ToDoDapperRepository : IToDoRepository
{
    private ILogger<ToDoDapperRepository> logger;
    private IDbConnectionFactory factory;

    public ToDoDapperRepository(ILogger<ToDoDapperRepository> logger, IDbConnectionFactory factory)
    {
        this.logger = logger;
        this.factory = factory;

        CreateTableIfNotExists();
    }

    public ToDo Add(ToDo entity)
    {
        using (var connection = factory.GetConnection())
        {
            connection.Execute<ToDo>(entity);
        }
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ToDo> GetAll(ToDoFilter filter)
    {
        using (var connection = factory.GetConnection())
        {
            var query = connection.QueryBuilder($"SELECT * FROM ToDo");
            if (filter.StartPage > -1)
                query += $"OFFSET {filter.ItemsPerPage * filter.StartPage} LIMIT {filter.ItemsPerPage}";

            return query.Query<ToDo>().ToList();
        }
    }

    public ToDo GetSingle(Guid id)
    {
        throw new NotImplementedException();
    }

    public ToDo Update(ToDo entity)
    {
        throw new NotImplementedException();
    }

    private void CreateTableIfNotExists()
    {
        var sql = $@"create table IF NOT EXISTS todo (
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