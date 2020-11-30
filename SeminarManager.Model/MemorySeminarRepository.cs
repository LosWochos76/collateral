using System.Collections.Generic;

namespace SeminarManager.Model
{
    public class MemorySeminarRepository : ISeminarRepository
    {
        private List<Seminar> objects = new List<Seminar>();
        
        public List<Seminar> All(int from = 0, int max = 1000)
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

        public Seminar ById(int id)
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

        public void Save(Seminar obj)
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
    }
}