using System;
using Npgsql;

namespace SeminarManager.SQL
{
    public class SqlRepositoryBase
    {
        protected NpgsqlConnection connection;

        public SqlRepositoryBase(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        protected bool TableExists(string table_name) 
        {
            var sql = "SELECT EXISTS (SELECT FROM pg_tables WHERE tablename=:table_name)";
            using (var cmd = new NpgsqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue(":table_name", table_name);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

        protected void Delete(string table_name, int id)
        {
            string sql = "delete FROM :table_name where id=:id";

            using (var cmd = new NpgsqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue(":table_name", table_name);
                cmd.Parameters.AddWithValue(":id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}