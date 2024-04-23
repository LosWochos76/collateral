using System;
using System.Collections.Generic;
using Npgsql;
using SeminarManager.Model;
using SimpleHashing.Net;

namespace SeminarManager.SQL
{
    public class SqlSeminarRepository : SqlRepositoryBase, ISeminarRepository
    {
        private ISimpleHash simpleHash = new SimpleHash();

        public SqlSeminarRepository(NpgsqlConnection connection) : base(connection)
        {
            if (!TableExists("sql_seminars"))
            {
                CreateTable();

                Save(new Seminar() {
                    Name = "Energy informatics",
                    Extent = "2L2E",
                    TeacherID = 1
                });

                Save(new Seminar() {
                    Name = "Objectoriented Programming",
                    Extent = "2L",
                    TeacherID = 1
                });
            }
        }

        private void CreateTable() 
        {
            string sql = "create table sql_seminars (" +
                "id serial primary key, " +
                "name varchar(100) not null, " +
                "extent varchar(100) not null, " +
                "teacher_id int, " +
                "constraint fk_teacher foreign key(teacher_id) references sql_persons(id))";

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
            obj.TeacherID = reader.GetInt32(3);
            return obj;
        }

        public List<Seminar> All(int from = 0, int max = 1000)
        {
            string sql = "SELECT id,name,extent,teacher_id FROM sql_seminars order by name " + 
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
            string sql = "SELECT id,name,extent,teacher_id FROM sql_seminars where id=:id";

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
            Delete("sql_seminars", id);
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
                "(name,extent,teacher_id) values (:name,:extent,:teacher_id) RETURNING id";

            using (var cmd = new NpgsqlCommand(sql, connection)) 
            {
                cmd.Parameters.AddWithValue(":name", obj.Name);
                cmd.Parameters.AddWithValue(":extent", obj.Extent);
                cmd.Parameters.AddWithValue(":teacher_id", obj.TeacherID);
                obj.ID = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void Update(Seminar obj)
        {
            string sql = "update sql_seminars set " +
                "name=:name, extent=:extent, teacher_id=:teacher_id where id=:id";

            using (var cmd = new NpgsqlCommand(sql, connection)) 
            {
                cmd.Parameters.AddWithValue(":id", obj.ID);
                cmd.Parameters.AddWithValue(":name", obj.Name);
                cmd.Parameters.AddWithValue(":extent", obj.Extent);
                cmd.Parameters.AddWithValue(":teacher_id", obj.TeacherID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}