using System;
using System.Collections.Generic;
using System.Linq;

namespace SeminarManager;

public class PersonRepository
{
    private List<Person> elements = new List<Person>();
    public IEnumerable<Person> Elements { get { return elements; } }

    public PersonRepository()
    {
    }

    public int Save(Person p)
    {
        if (p.ID == 0)
        {
            p.ID = elements.Count + 1;
            elements.Add(p);
        }

        return p.ID;
    }

    public void RemoveById(int id)
    {
        var obj = elements.Where(item => item.ID == id).FirstOrDefault();
        elements.Remove(obj);
    }
}
