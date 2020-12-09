using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace SeminarManager.Model
{
    public class MemoryPersonRepository : IPersonRepository
    {
        private List<Person> objects = new List<Person>();

        public List<Person> All(int from = 0, int max = 1000)
        {
            return objects;
        }

        private int PosOf(int id)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].ID == id)
                    return i;
            }

            return -1;
        }

        public Person ById(int id)
        {
            var pos = PosOf(id);
            if (pos != -1)
                return objects[pos];
            else
                return null;
        }

        public void Delete(int id)
        {
            var pos = PosOf(id);
            if (pos != -1)
                objects.RemoveAt(pos);
        }

        public void Save(Person obj)
        {
            if (obj.ID == 0)
            {
                obj.ID = objects.Count + 1;
                objects.Add(obj);
            }
            else
            {
                var pos = PosOf(obj.ID);
                objects[pos] = obj;
            }
        }

        public Person FindAdminByEmailAndPassword(string email, string password)
        {
            return (from obj in objects 
                where obj.IsAdmin && obj.EMail.Equals(email) && obj.Password.Equals(password) 
                select obj).SingleOrDefault();
        }
    }
}