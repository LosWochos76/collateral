using System;
using Microsoft.EntityFrameworkCore;
using SeminarManager.Model;

namespace SeminarManager.EF
{
    public class SeminarManagerContext : DbContext
    {
        private string connection_string;
        public DbSet<Person> Persons { get; set; }
        public DbSet<Seminar> Seminars { get; set; }

        public SeminarManagerContext()
        {
            connection_string = string.Format("Host={0};Username={1};Password={2};Database={3}",
                GetFromEnvironmentOrDefault("POSTGRESQL_HOST", "localhost"),
                GetFromEnvironmentOrDefault("POSTGRESQL_USER", "postgres"),
                GetFromEnvironmentOrDefault("POSTGRESQL_PASSWORD", "secret"),
                GetFromEnvironmentOrDefault("POSTGRESQL_DATABASE", "postgres"));
        }

        private string GetFromEnvironmentOrDefault(string key, string def)
        {
            string var = Environment.GetEnvironmentVariable(key);
            if (var == null || var == string.Empty)
                return def;
            else
                return var;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connection_string);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Person>();
            modelBuilder.Entity<Seminar>();
        }
    } 
}