using System;
using Npgsql;
using SeminarManager.Model;

namespace SeminarManager.SQL
{
    public class SqlRepository : IRepository, IDisposable
    {
        private NpgsqlConnection connection;
        private SqlPersonRepository person_repository;
        private SqlSeminarRepository seminar_repository;

        private string GetFromEnvironmentOrDefault(string key, string def)
        {
            string var = Environment.GetEnvironmentVariable(key);
            if (var == null || var == string.Empty)
                return def;
            else
                return var;
        }

        public SqlRepository() 
        {
            var connection_string = string.Format("Host={0};Username={1};Password={2};Database={3}",
                GetFromEnvironmentOrDefault("POSTGRESQL_HOST", "localhost"),
                GetFromEnvironmentOrDefault("POSTGRESQL_USER", "postgres"),
                GetFromEnvironmentOrDefault("POSTGRESQL_PASSWORD", "secret"),
                GetFromEnvironmentOrDefault("POSTGRESQL_DATABASE", "postgres"));

            connection = new NpgsqlConnection(connection_string);
            connection.Open();

            person_repository = new SqlPersonRepository(connection);
            seminar_repository = new SqlSeminarRepository(connection);
        }

        public IPersonRepository Persons 
        {
            get { return person_repository; }
        }

        public ISeminarRepository Seminars 
        {
            get { return seminar_repository; }
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}