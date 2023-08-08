public class BankkontoEventArgs : EventArgs
{
    public double Betrag { get; set; }
    public bool WarEineEinzahlung { get; set; }

    public BankkontoEventArgs(double betrag, bool einzahlung)
    {
        Betrag = betrag;
        WarEineEinzahlung = einzahlung;
    }

    public override string ToString()
    {
        return WarEineEinzahlung ? "Einzahlung" : "Auszahlung" + 
            string.Format(" von {0:C}.", Betrag);
    }
}