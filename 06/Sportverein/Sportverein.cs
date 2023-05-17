using System;
using System.Collections.Generic;

class Sportverein
{
    private Mitglied vorsitzender;
    private List<Mitglied> mitglieder = new List<Mitglied>();

    public string Name { get; set; }

    public Mitglied Vorsitzender
    {
        get { return vorsitzender; }
        set
        {
            if (value == null)
                throw new Exception("Der Verein muss einen Vorsitzenden haben!");

            vorsitzender = value;
        }
    }

    public void Ausgeben()
    {
        Console.WriteLine("Verein " + Name);
        Console.WriteLine("Vorsitzender: " + Vorsitzender);
        Console.WriteLine("Mitglieder:");
        
        foreach (var m in AlleMitglieder())
        {
            Console.WriteLine("Mitglied: " + m);
        }
    }

    public Sportverein(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

        Name = name;
    }

    public void MitgliedHinzufuegen(Mitglied m)
    {
        mitglieder.Add(m);
    }

    public IEnumerable<Mitglied> AlleMitglieder()
    {
        return mitglieder;
    }
}