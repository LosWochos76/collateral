public static class Duplikat
{
    // O(n^2)
    public static bool MitSchleifen(int[] zahlen)
    {
        for (int i = 0; i < zahlen.Length; i++)
            for (int j = i + 1; j < zahlen.Length; j++)
                if (zahlen[i] == zahlen[j])
                    return true;
        return false;
    }

    // O(n log n): Nach dem Sortieren liegen Duplikate nebeneinander
    public static bool MitSortieren(int[] zahlen)
    {
        var kopie = (int[])zahlen.Clone();
        Array.Sort(kopie);
        for (int i = 1; i < kopie.Length; i++)
            if (kopie[i] == kopie[i - 1])
                return true;
        return false;
    }

    // O(n): Add liefert false, wenn der Wert schon enthalten ist
    public static bool MitHashSet(int[] zahlen)
    {
        var gesehen = new HashSet<int>();
        foreach (var z in zahlen)
            if (!gesehen.Add(z))
                return true;
        return false;
    }
}

public record Kunde(int Id, string Name);
public record Bestellung(int Nummer, int KundenId);

public static class Join
{
    // O(n * m)
    public static List<string> MitListe(List<Kunde> kunden, List<Bestellung> bestellungen)
    {
        var result = new List<string>();
        foreach (var b in bestellungen)
        {
            var kunde = kunden.Find(k => k.Id == b.KundenId)!;
            result.Add($"{b.Nummer}: {kunde.Name}");
        }
        return result;
    }

    // O(n + m)
    public static List<string> MitDictionary(List<Kunde> kunden, List<Bestellung> bestellungen)
    {
        var kundeNachId = kunden.ToDictionary(k => k.Id);
        var result = new List<string>();
        foreach (var b in bestellungen)
            result.Add($"{b.Nummer}: {kundeNachId[b.KundenId].Name}");
        return result;
    }
}

public static class TwoSum
{
    // O(n^2)
    public static (int, int)? BruteForce(int[] a, int ziel)
    {
        for (int i = 0; i < a.Length; i++)
            for (int j = i + 1; j < a.Length; j++)
                if (a[i] + a[j] == ziel)
                    return (i, j);
        return null;
    }

    // O(n): Wir merken uns zu jedem Wert seinen Index
    public static (int, int)? MitDictionary(int[] a, int ziel)
    {
        var gesehen = new Dictionary<int, int>();
        for (int i = 0; i < a.Length; i++)
        {
            if (gesehen.TryGetValue(ziel - a[i], out int j))
                return (j, i);
            gesehen[a[i]] = i;
        }
        return null;
    }
}
