public class Csv
{
    public static double LeseGesamtumsatz(string dateiname)
    {
        var zeilen = File.ReadAllLines(dateiname);
        double gesamtumsatz = 0;

        for (int i=1; i<zeilen.Length; i++)
        {
            string[] bestandteile = zeilen[i].Split(";");
            string produktname = bestandteile[0];
            double preis = Convert.ToDouble(bestandteile[1]);
            int anzahl = Convert.ToInt32(bestandteile[2]);
            gesamtumsatz += preis * anzahl;
        }

        return gesamtumsatz;
    }
}