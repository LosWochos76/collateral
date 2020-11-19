using System.Collections.Generic;

namespace SeminarManager.Model
{
    public class MemoryPersonRepository : IPersonRepository
    {
        private List<Person> objects = new List<Person>();

        public MemoryPersonRepository()
        {
            Save(new Person() { Firstname = "Alex", Lastname = "Stuckenholz", IsAdmin = true, EMail = "alexander.stuckenholz@hshl.de", Password = "test" });
            Save(new Person() { Firstname = "Anne", Lastname = "Meier", IsAdmin = false });
        }

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

        public int Count
        {
            get
            {
                return objects.Count;
            }
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
                obj.ID = Count + 1;
                objects.Add(obj);
            }
            else
            {
                var pos = PosOf(obj.ID);
                objects[pos] = obj;
            }
        }

        public Person FindAdminByEmailAndPassword(LoginModel login)
        {
            foreach (var obj in objects)
            {
                if (obj.IsAdmin && obj.EMail.Equals(login.Email) && obj.Password.Equals(login.Password))
                    return obj;
            }

            return null;
        }
    }
}