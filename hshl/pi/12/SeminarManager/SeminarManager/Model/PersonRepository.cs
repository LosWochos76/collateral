using System.Collections.Generic;

namespace SeminarManager;

public class PersonRepository
{
    private Dictionary<int, Person> elements = new ();
    public IEnumerable<Person> Elements { get { return elements.Values; } }

    public PersonRepository()
    {
    }

    public int Save(Person obj)
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
