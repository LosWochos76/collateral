using System.Text.RegularExpressions;

public static class Textanalyse
{
    public static List<string> Woerter(string text)
    {
        return Regex.Matches(text.ToLower(), @"\p{L}+")
            .Select(m => m.Value)
            .ToList();
    }

    // Muster "Menge": Wie viele verschiedene Wörter gibt es?
    public static int AnzahlVerschiedene(List<string> woerter)
    {
        return new HashSet<string>(woerter).Count;
    }

    // Muster "Zählen": Wie oft kommt jedes Wort vor?
    public static Dictionary<string, int> Haeufigkeiten(List<string> woerter)
    {
        var haeufigkeit = new Dictionary<string, int>();
        foreach (var w in woerter)
            haeufigkeit[w] = haeufigkeit.GetValueOrDefault(w) + 1;
        return haeufigkeit;
    }

    public static IEnumerable<KeyValuePair<string, int>> Top(Dictionary<string, int> haeufigkeit, int k)
    {
        return haeufigkeit.OrderByDescending(p => p.Value).Take(k);
    }

    // Muster "Gruppieren": Wörter mit denselben Buchstaben sind Anagramme
    public static List<List<string>> Anagramme(IEnumerable<string> woerter)
    {
        var gruppen = new Dictionary<string, List<string>>();
        foreach (var w in new HashSet<string>(woerter))
        {
            var key = new string(w.OrderBy(c => c).ToArray());
            if (!gruppen.TryGetValue(key, out var liste))
                gruppen[key] = liste = new List<string>();
            liste.Add(w);
        }

        return gruppen.Values.Where(g => g.Count > 1).ToList();
    }
}
