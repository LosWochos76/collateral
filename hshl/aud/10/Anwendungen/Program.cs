// Anwendungen von Dictionary und HashSet
// Aufruf: dotnet run [-- pfad/zu/text.txt]

string datei = args.Length > 0 ? args[0] : Path.Combine(AppContext.BaseDirectory, "beispiel.txt");
var woerter = Textanalyse.Woerter(File.ReadAllText(datei));

Console.WriteLine($"=== Textanalyse von {Path.GetFileName(datei)} ===");
Console.WriteLine($"Wörter insgesamt: {woerter.Count}");
Console.WriteLine($"Verschiedene Wörter: {Textanalyse.AnzahlVerschiedene(woerter)}");

Console.WriteLine("Die 10 häufigsten Wörter:");
foreach (var p in Textanalyse.Top(Textanalyse.Haeufigkeiten(woerter), 10))
    Console.WriteLine($"  {p.Key,-15} {p.Value}");

Console.WriteLine("Anagramme:");
foreach (var gruppe in Textanalyse.Anagramme(woerter))
    Console.WriteLine("  " + string.Join(", ", gruppe));

Console.WriteLine();
Console.WriteLine("=== Mengenoperationen ===");
var termin1 = new HashSet<int> { 4711, 815, 1234, 4242 };
var termin2 = new HashSet<int> { 4711, 1234, 9999 };

var beide = new HashSet<int>(termin1);
beide.IntersectWith(termin2);
var mindestensEinmal = new HashSet<int>(termin1);
mindestensEinmal.UnionWith(termin2);
var nurTermin1 = new HashSet<int>(termin1);
nurTermin1.ExceptWith(termin2);

Console.WriteLine($"An beiden Terminen: {string.Join(", ", beide)}");
Console.WriteLine($"Mindestens einmal:  {string.Join(", ", mindestensEinmal)}");
Console.WriteLine($"Nur an Termin 1:    {string.Join(", ", nurTermin1)}");

Console.WriteLine();
Console.WriteLine("=== Two-Sum ===");
int[] zahlen = { 8, 3, 11, 5, 7 };
Console.WriteLine($"Brute Force: {TwoSum.BruteForce(zahlen, 12)}");
Console.WriteLine($"Dictionary:  {TwoSum.MitDictionary(zahlen, 12)}");

Console.WriteLine();
Console.WriteLine("=== Fallstricke ===");
Fallstricke.Zeigen();

Console.WriteLine();
Console.WriteLine("=== Laufzeitmessungen ===");
Messung.Rechtschreibpruefung();
Messung.Duplikate();
Messung.Nachschlagen();
