using Microsoft.EntityFrameworkCore;
using ToDoManager.Common.Models;

namespace ToDoManager.Persistence.EfCore;

public class ApplicationDbContext : DbContext
{
    private DbConnectionFactory dbConnectionFactory;
    public DbSet<ToDo> ToDos { get; set; }
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbConnectionFactory connectionFactory)
    {
        this.dbConnectionFactory = connectionFactory;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (!optionsBuilder.IsConfigured)
        {
            var connection = dbConnectionFactory.GetConnection();
            optionsBuilder.UseNpgsql(connection);
            optionsBuilder.UseLowerCaseNamingConvention();
        }
    }

    public void Migrate()
    {
        if (Database.GetPendingMigrations().Any())
            Database.Migrate();
    }
}