using Dapper;
using MediatR;
using ToDoManager.Common.Models;
using ToDoManager.Persistence;

namespace ToDoManager.CQRS;

public sealed record GetSingleToDoByIdQuery(Guid Id) : IRequest<ToDo>;

public sealed class GetSingleToDoQueryHandler : IRequestHandler<GetSingleToDoByIdQuery, ToDo>
{
    private DbConnectionFactory dbConnectionFactory;

    public GetSingleToDoQueryHandler(DbConnectionFactory dbConnectionFactory)
    {
        this.dbConnectionFactory = dbConnectionFactory;
    }

    async Task<ToDo> IRequestHandler<GetSingleToDoByIdQuery, ToDo>.Handle(GetSingleToDoByIdQuery request, CancellationToken cancellationToken)
    {
        var sql = "SELECT * FROM ToDos WHERE ID = @Id";
        using (var connection = dbConnectionFactory.GetConnection())
            return connection.QuerySingleOrDefault<ToDo>(sql, new { Id = request.Id });
    }
}