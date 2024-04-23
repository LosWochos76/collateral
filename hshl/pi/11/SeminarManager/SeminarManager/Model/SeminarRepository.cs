using System.Collections.Generic;
using System.Linq;

namespace SeminarManager;

public class SeminarRepository
{
    private List<Seminar> elements = new List<Seminar>();
    public IEnumerable<Seminar> Elements { get { return elements; } }

    public SeminarRepository()
    {
    }

    public int Save(Seminar s)
    {
        if (s.ID == 0)
        {
            s.ID = elements.Count + 1;
            elements.Add(s);
        }

        return s.ID;
    }

    public void RemoveById(int id)
    {
        var obj = elements.Where(item => item.ID == id).FirstOrDefault();
        elements.Remove(obj);
    }
}
