namespace OOP
{
    public class Bankkonto
    {
        public int Kontonummer { get; }
        public double Kontostand { get; set; }
        public Bankkunde Besitzer { get; }

        public Bankkonto(Bankkunde besitzer, int kontonummer)
        {
            this.Besitzer = besitzer;
            this.Kontonummer = kontonummer;
            this.Kontostand = 0;
        }
    }
}