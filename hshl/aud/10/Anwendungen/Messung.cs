using System.Diagnostics;

// Laufzeitvergleich: List gegen HashSet bzw. Dictionary
public static class Messung
{
    private static readonly Random random = new Random(42);

    private static string ZufallsWort()
    {
        int length = random.Next(4, 12);
        var chars = new char[length];
        for (int i = 0; i < length; i++)
            chars[i] = (char)('a' + random.Next(26));
        return new string(chars);
    }

    private static long Millisekunden(Action action)
    {
        var watch = Stopwatch.StartNew();
        action();
        return watch.ElapsedMilliseconds;
    }

    // Rechtschreibprüfung: Jedes Wort eines Textes wird im Wörterbuch gesucht
    public static void Rechtschreibpruefung(int anzahlWoerter = 100_000, int laengeText = 10_000)
    {
        var woerter = new List<string>();
        for (int i = 0; i < anzahlWoerter; i++)
            woerter.Add(ZufallsWort());

        var text = new List<string>();
        for (int i = 0; i < laengeText; i++)
            text.Add(i % 2 == 0 ? woerter[random.Next(woerter.Count)] : ZufallsWort());

        var liste = new List<string>(woerter);
        var menge = new HashSet<string>(woerter);
        int unbekanntListe = 0, unbekanntMenge = 0;

        long msListe = Millisekunden(() =>
        {
            foreach (var w in text)
                if (!liste.Contains(w)) unbekanntListe++;
        });

        long msMenge = Millisekunden(() =>
        {
            foreach (var w in text)
                if (!menge.Contains(w)) unbekanntMenge++;
        });

        Console.WriteLine($"Rechtschreibprüfung ({anzahlWoerter} Wörter, Text mit {laengeText} Wörtern):");
        Console.WriteLine($"  List:    {msListe,6} ms, {unbekanntListe} unbekannte Wörter");
        Console.WriteLine($"  HashSet: {msMenge,6} ms, {unbekanntMenge} unbekannte Wörter");
    }

    // Duplikate finden: O(n^2), O(n log n) und O(n)
    public static void Duplikate(int n = 100_000)
    {
        // Ungünstigster Fall: n verschiedene Zahlen in zufälliger Reihenfolge
        var zahlen = new int[n];
        for (int i = 0; i < n; i++)
            zahlen[i] = i;
        random.Shuffle(zahlen);

        bool a = false, b = false, c = false;
        long msSchleife = Millisekunden(() => a = Duplikat.MitSchleifen(zahlen));
        long msSortiert = Millisekunden(() => b = Duplikat.MitSortieren(zahlen));
        long msMenge = Millisekunden(() => c = Duplikat.MitHashSet(zahlen));

        Console.WriteLine($"Duplikate in {n} Zahlen finden:");
        Console.WriteLine($"  Zwei Schleifen: {msSchleife,6} ms ({a})");
        Console.WriteLine($"  Sortieren:      {msSortiert,6} ms ({b})");
        Console.WriteLine($"  HashSet:        {msMenge,6} ms ({c})");
    }

    // Nachschlagen: Zu jeder Bestellung den Kunden finden
    public static void Nachschlagen(int n = 20_000)
    {
        var kunden = new List<Kunde>();
        var bestellungen = new List<Bestellung>();
        for (int i = 0; i < n; i++)
        {
            kunden.Add(new Kunde(i, "Kunde " + i));
            bestellungen.Add(new Bestellung(i, random.Next(n)));
        }

        int x = 0, y = 0;
        long msListe = Millisekunden(() => x = Join.MitListe(kunden, bestellungen).Count);
        long msDict = Millisekunden(() => y = Join.MitDictionary(kunden, bestellungen).Count);

        Console.WriteLine($"Kunden zu {n} Bestellungen nachschlagen:");
        Console.WriteLine($"  List.Find:  {msListe,6} ms ({x} Zeilen)");
        Console.WriteLine($"  Dictionary: {msDict,6} ms ({y} Zeilen)");
    }
}
