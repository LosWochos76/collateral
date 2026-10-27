// Ohne Equals und GetHashCode wird nur die Referenz verglichen
public class PunktKlasse
{
    public int X { get; }
    public int Y { get; }

    public PunktKlasse(int x, int y)
    {
        X = x;
        Y = y;
    }
}

// Ein record erzeugt Equals und GetHashCode aus allen Eigenschaften
public record PunktRecord(int X, int Y);

// Veränderlicher Schlüssel
public class Student
{
    public int Matrikelnummer { get; set; }

    public override bool Equals(object? obj) =>
        obj is Student s && s.Matrikelnummer == Matrikelnummer;

    public override int GetHashCode() => Matrikelnummer.GetHashCode();
}

public static class Fallstricke
{
    public static void Zeigen()
    {
        var klassen = new HashSet<PunktKlasse> { new PunktKlasse(1, 2), new PunktKlasse(1, 2) };
        var records = new HashSet<PunktRecord> { new PunktRecord(1, 2), new PunktRecord(1, 2) };
        Console.WriteLine($"HashSet mit class:  Count = {klassen.Count}");
        Console.WriteLine($"HashSet mit record: Count = {records.Count}");

        var student = new Student { Matrikelnummer = 4711 };
        var angemeldet = new HashSet<Student> { student };
        student.Matrikelnummer = 815; // Schlüssel nach dem Einfügen verändert!
        Console.WriteLine($"Student nach Änderung gefunden: {angemeldet.Contains(student)}");

        var noten = new Dictionary<string, double> { ["Mia"] = 1.7 };
        try
        {
            Console.WriteLine(noten["Noah"]);
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("noten[\"Noah\"] wirft eine KeyNotFoundException");
        }

        if (!noten.TryGetValue("Noah", out double note))
            Console.WriteLine("TryGetValue liefert stattdessen false");
    }
}
