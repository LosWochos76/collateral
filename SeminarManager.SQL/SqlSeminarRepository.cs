using System;
using System.Collections.Generic;
using Npgsql;
using SeminarManager.Model;
using SimpleHashing.Net;

namespace SeminarManager.SQL
{
    public class SqlSeminarRepository : ISeminarRepository
    {
        private ISimpleHash simpleHash = new SimpleHash();
        private NpgsqlConnection connection;

        public SqlSeminarRepository(NpgsqlConnection connection)
        {
            this.connection = connection;

            if (!TableExists())
                CreateTable();
        }

        private bool TableExists() 
        {
            var sql = "SELECT EXISTS (SELECT FROM pg_tables WHERE tablename = 'sql_seminars')";

            using (var cmd = new NpgsqlCommand(sql, connection))
            {
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

        private void CreateTable() 
        {
            string sql = "create table sql_seminars (" +
                "id serial primary key, " +
                "name varchar(100) not null, " +
                "extent varchar(100) not null)";

            using (var cmd = new NpgsqlCommand(sql, connection)) 
            {
                cmd.ExecuteNonQuery();
            }
        }

        private Seminar FromReader(NpgsqlDataReader reader)
        {
            var obj = new Seminar();
            obj.ID = reader.GetInt32(0);
            obj.Name = reader.GetString(1);
            obj.Extent = reader.GetString(2);
            return obj;
        }

        public List<Seminar> All(int from = 0, int max = 1000)
        {
            string sql = "SELECT id,name,extent FROM sql_seminars " + 
                "limit :max offset :from";

            var cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue(":from", from);
            cmd.Parameters.AddWithValue(":max", max);

            var result = new List<Seminar>();
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var obj = FromReader(reader);
                    result.Add(obj);
                }
            }

            return result;
        }

        public Seminar ById(int id)
        {
            string sql = "SELECT id,name,extent FROM sql_seminars where id=:id";

            using (var cmd = new NpgsqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue(":id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return FromReader(reader);
                    }
                }
            }

            return null;
        }

        public void Delete(int id)
        {
            string sql = "delete FROM sql_seminars where id=:id";

            using (var cmd = new NpgsqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue(":id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Save(Seminar obj)
        {
            if (obj.ID == 0)
                SaveNew(obj);
            else
                Update(obj);
        }

        private void SaveNew(Seminar obj)
        {
            string sql = "insert into sql_seminars " +
                "(name,extent) values (:name,:extent) RETURNING id";

            using (var cmd = new NpgsqlCommand(sql, connection)) 
            {
                cmd.Parameters.AddWithValue(":name", obj.Name);
                cmd.Parameters.AddWithValue(":extent", obj.Extent);
                obj.ID = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void Update(Seminar obj)
        {
            string sql = "update sql_seminars set " +
                "name=:name, extent=:extent where id=:id";

            using (var cmd = new NpgsqlCommand(sql, connection)) 
            {
                cmd.Parameters.AddWithValue(":id", obj.ID);
                cmd.Parameters.AddWithValue(":name", obj.Name);
                cmd.Parameters.AddWithValue(":extent", obj.Extent);
                cmd.ExecuteNonQuery();
            }
        }
    }
}