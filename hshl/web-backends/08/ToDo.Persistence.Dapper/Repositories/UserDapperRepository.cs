using Dapper;
using DapperQueryBuilder;
using Microsoft.Extensions.Logging;
using ToDoManager.Common.Misc;
using ToDoManager.Common.Models;

namespace ToDoManager.Persistence.Dapper.Repositories;

public class UserDapperRepository : IUserRepository
{
    private ILogger<ToDoDapperRepository> logger;
    private IDbConnectionFactory factory;
    private PasswordHelper passwordHelper;

    public UserDapperRepository(ILogger<ToDoDapperRepository> logger, IDbConnectionFactory factory, PasswordHelper passwordHelper)
    {
        this.logger = logger;
        this.factory = factory;
        this.passwordHelper = passwordHelper;

        /*CreateTableIfNotExists();

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
        }*/
    }

    public User Add(User entity)
    {
        using (var connection = factory.GetConnection())
        {
            var sql = "INSERT INTO Users (ID, Firstname, Lastname, EMail, PasswordHash, IsAdmin, PasswordResetToken) VALUES (@ID, @Firstname, @Lastname, @EMail, @PasswordHash, @IsAdmin, @PasswordResetToken) RETURNING *";
            return connection.QuerySingle<User>(sql, entity);
        }
    }

    public void Delete(Guid id)
    {
        using (var connection = factory.GetConnection())
        {
            var sql = "DELETE FROM Users WHERE ID = @Id";
            connection.Execute(sql, new { Id = id });
        }
    }

    public IEnumerable<User> GetAll()
    {
        using (var connection = factory.GetConnection())
        {
            var sql = "SELECT * FROM Users";
            return connection.Query<User>(sql).ToList();
        }
    }

    public User GetSingle(Guid id)
    {
        using (var connection = factory.GetConnection())
        {
            var sql = "SELECT * FROM Users WHERE ID = @Id";
            return connection.QuerySingleOrDefault<User>(sql, new { Id = id });
        }
    }

    public User Update(User entity)
    {
        using (var connection = factory.GetConnection())
        {
            var sql = "UPDATE Users SET Firstname = @Firstname, Lastname = @Lastname, EMail = @EMail, PasswordHash = @PasswordHash, IsAdmin = @IsAdmin, PasswordResetToken = @PasswordResetToken WHERE ID = @ID RETURNING *";
            return connection.QuerySingle<User>(sql, entity);
        }
    }

    public User FindByEmail(string email)
    {
        using (var connection = factory.GetConnection())
        {
            var sql = "SELECT * FROM Users WHERE EMail ilike @EMail";
            return connection.QuerySingleOrDefault<User>(sql, new { EMail = email });
        }
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
        using (var connection = factory.GetConnection())
        {
            var sql = "SELECT * FROM Users WHERE PasswordResetToken = @PasswordResetToken";
            return connection.QuerySingleOrDefault<User>(sql, new { PasswordResetToken = token });
        }
    }

    private void CreateTableIfNotExists()
    {
        using (var connection = factory.GetConnection())
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
            connection.Execute(sql);
        }
    }
}