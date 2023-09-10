using System.Collections.Generic;

namespace SeminarManager;

public class SeminarRepository
{
    private Dictionary<int, Seminar> elements = new();
    public IEnumerable<Seminar> Elements { get { return elements.Values; } }

    public SeminarRepository()
    {
    }

    public int Save(Seminar obj)
    {
        if (obj.ID == 0)
            obj.ID = elements.Count + 1;

        elements[obj.ID] = obj;
        return obj.ID;
    }

    public void RemoveById(int id)
    {
        elements.Remove(id);
    }

    public void Clear()
    {
        elements.Clear();
    }
}
