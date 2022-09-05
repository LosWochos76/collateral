public class Program
{
    public static void Main(string[] args)
    {
        int[] zahlen = ArrayHelper.Zufallszahlen(5000);
        Console.WriteLine(ArrayHelper.Min(zahlen));
        Console.WriteLine(ArrayHelper.Summe(zahlen));
        Console.WriteLine(ArrayHelper.ArithmetischerMittelwert(zahlen));

        TicTacToe.LoescheDasSpielfeld();
        TicTacToe.GibSpielbrettAus();
    }
}