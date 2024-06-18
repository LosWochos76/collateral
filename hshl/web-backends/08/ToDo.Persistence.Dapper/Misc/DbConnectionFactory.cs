using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;

namespace ToDoManager.Persistence.Dapper.Misc;

public interface IDbConnectionFactory
{
    NpgsqlConnection GetConnection();
}

public class DbConnectionFactory : IDbConnectionFactory
{
    private ILogger<DbConnectionFactory> logger;
    private DatabaseSettings settings;

    public DbConnectionFactory(ILogger<DbConnectionFactory> logger, IOptions<DatabaseSettings> settings)
    {
        this.logger = logger;
        this.settings = settings.Value;
    }

    public NpgsqlConnection GetConnection()
    {
        var connection = new NpgsqlConnection(settings.ConnectionString);
        connection.Open();
        return connection;
    }
}