using System.Collections.Generic;
using Npgsql;
using SeminarManager.Model;

namespace SeminarManager.SQL
{
    public class SqlAttendeeRepository : SqlRepositoryBase, IAttendeeRepository
    {
        public SqlAttendeeRepository(NpgsqlConnection connection) : base(connection)
        {
            if (!TableExists("sql_attendees")) 
            {
                CreateTable();
                Save(new Seminar(){ ID=1 }, new List<int>() { 2 });
                Save(new Seminar(){ ID=2 }, new List<int>() { 2 });
            }
        }

        private void CreateTable()
        {
            string sql = "create table sql_attendees (" +
                "person_id int, " +
                "seminar_id int, " +
                "constraint fk_person foreign key(person_id) references sql_persons(id), " + 
                "constraint fk_seminar foreign key(seminar_id) references sql_seminars(id))";

            using (var cmd = new NpgsqlCommand(sql, connection)) 
            {
                cmd.ExecuteNonQuery();
            }
        }
        
        public List<int> Get(Seminar seminar)
        {
            var result = new List<int>();

            string sql = "SELECT person_id FROM sql_attendees where seminar_id=:seminar_id";
            using (var cmd = new NpgsqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue(":seminar_id", seminar.ID);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        result.Add(reader.GetInt32(0));
                }
            }

            return result;
        }

        public void Save(Seminar seminar, List<int> attendees)
        {
            RemoveAll(seminar.ID);
            foreach (int person_id in attendees)
                InsertSingle(seminar.ID, person_id);
        }

        private void RemoveAll(int seminar_id)
        {
            string sql = "delete from sql_attendees where seminar_id=:seminar_id";

            using (var cmd = new NpgsqlCommand(sql, connection)) 
            {
                cmd.Parameters.AddWithValue(":seminar_id", seminar_id);
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertSingle(int seminar_id, int person_id)
        {
            string sql = "insert into sql_attendees " +
                "(person_id,seminar_id) values (:person_id,:seminar_id)";

            using (var cmd = new NpgsqlCommand(sql, connection)) 
            {
                cmd.Parameters.AddWithValue(":person_id", person_id);
                cmd.Parameters.AddWithValue(":seminar_id", seminar_id);
                cmd.ExecuteScalar();
            }
        }
    }
}