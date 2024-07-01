using Dapper;
using DapperQueryBuilder;
using Microsoft.Extensions.Logging;
using ToDoManager.Common.Misc;
using ToDoManager.Common.Models;

namespace ToDoManager.Persistence.Dapper.Repositories;

public class UserDapperRepository : IUserRepository
{
    private ILogger<ToDoDapperRepository> logger;
    private DbConnectionFactory factory;
    private PasswordHelper passwordHelper;

    public UserDapperRepository(ILogger<ToDoDapperRepository> logger, DbConnectionFactory factory, PasswordHelper passwordHelper)
    {
        this.logger = logger;
        this.factory = factory;
        this.passwordHelper = passwordHelper;

        CreateTableIfNotExists();

        if (GetAll().Count() == 0)
        {
            Add(new User() 
            { 
                Firstname = "Alexander",
                Lastname = "Stuckenholz",
                EMail="Alexander.Stuckenholz@hshl.de", 
                IsAdmin = true,
                PasswordHash = passwordHelper.ComputeSha256Hash("secret")
            });
        }
    }

    public User Add(User entity)
    {
        var sql = @"INSERT INTO Users (ID, Firstname, Lastname, EMail, PasswordHash, IsAdmin, PasswordResetToken) 
            VALUES (@ID, @Firstname, @Lastname, @EMail, @PasswordHash, @IsAdmin, @PasswordResetToken) RETURNING *";

        using (var connection = factory.GetConnection())
            return connection.QuerySingle<User>(sql, entity);
    }

    public void Delete(Guid id)
    {
        var sql = "DELETE FROM Users WHERE ID = @Id";
        using (var connection = factory.GetConnection())
            connection.Execute(sql, new { Id = id });
    }

    public IEnumerable<User> GetAll()
    {
        var sql = "SELECT * FROM Users";
        using (var connection = factory.GetConnection())
            return connection.Query<User>(sql).ToList();
    }

    public User GetSingle(Guid id)
    {
        var sql = "SELECT * FROM Users WHERE ID = @Id";
        using (var connection = factory.GetConnection())
            return connection.QuerySingleOrDefault<User>(sql, new { Id = id });
    }

    public User Update(User entity)
    {
        var sql = @"UPDATE Users SET Firstname = @Firstname, Lastname = @Lastname, EMail = @EMail, PasswordHash = @PasswordHash, 
            IsAdmin = @IsAdmin, PasswordResetToken = @PasswordResetToken WHERE ID = @ID RETURNING *";
        
        using (var connection = factory.GetConnection())
            return connection.QuerySingle<User>(sql, entity);
    }

    public User FindByEmail(string email)
    {
        var sql = "SELECT * FROM Users WHERE EMail ilike @EMail";
        using (var connection = factory.GetConnection())
            return connection.QuerySingleOrDefault<User>(sql, new { EMail = email });
    }

    public User FindByLogin(string email, string password)
    {
        var user = FindByEmail(email);
        if (user is null)
            return null;

        if (user.PasswordHash.Equals(passwordHelper.ComputeSha256Hash(password)))
            return user;

        return null;
    }

    public User FindByPasswordResetToken(string token)
    {
        var sql = "SELECT * FROM Users WHERE PasswordResetToken = @PasswordResetToken";
        using (var connection = factory.GetConnection())
            return connection.QuerySingleOrDefault<User>(sql, new { PasswordResetToken = token });
    }

    private void CreateTableIfNotExists()
    {
        var sql = @"
            CREATE TABLE IF NOT EXISTS Users (
                ID UUID PRIMARY KEY,
                Firstname VARCHAR(255) NOT NULL,
                Lastname VARCHAR(255) NOT NULL,
                EMail VARCHAR(255) NOT NULL UNIQUE,
                PasswordHash VARCHAR(255) NOT NULL,
                IsAdmin BOOLEAN NOT NULL,
                PasswordResetToken VARCHAR(255)
            )";

        using (var connection = factory.GetConnection())
            connection.Execute(sql);
    }
}