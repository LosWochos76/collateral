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
}