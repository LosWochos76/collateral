var hvn = HausVomNikolaus.Erzeuge();
var ew = new EulerWeg(hvn);
var solutions = ew.FindSolutions();

foreach (var s in solutions)
    Console.WriteLine(s);
