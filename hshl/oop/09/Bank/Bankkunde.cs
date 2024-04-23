using System;
using System.Collections.Generic;

public class Bankkunde
{
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    private List<Bankkonto> konten = new List<Bankkonto>();

    public Bankkunde(string vorname, string nachname, Bankkonto konto)
    {
        Vorname = vorname;
        Nachname = nachname;
        konten.Add(konto);
    }

    public void KontoHinzufuegen(Bankkonto konto)
    {
        konten.Add(konto);
    }

    public void Ausgeben()
    {
        Console.WriteLine(Vorname + " " + Nachname);
        for (int i=0; i<konten.Count; i++)
            Console.WriteLine("Kontostand für Konto #" + (i+1) + ": " + konten[i].Kontostand);
    }
}