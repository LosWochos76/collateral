using System;

class Adresse
{
    public string Strasse { get; set; }
    public int PLZ { get; set; }
    public string Ort { get; set; }

    public Adresse(string strasse, int plz, string ort)
    {
        this.Strasse = strasse;
        this.PLZ = plz;
        this.Ort = ort;
    }

    public void Ausgeben()
    {
        Console.WriteLine(Strasse);
        Console.WriteLine(PLZ + " " + Ort);
    }
}