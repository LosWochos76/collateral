public class Program
{
    public static void Main(string[] args)
    {
        /*
        int[] zahlen = ArrayHelper.Zufallszahlen(5000);
        Console.WriteLine(ArrayHelper.Min(zahlen));
        Console.WriteLine(ArrayHelper.Summe(zahlen));
        Console.WriteLine(ArrayHelper.ArithmetischerMittelwert(zahlen));
        */

        /*
        int[] fib = Fibonacci.Fill(10);
        for (int i=0; i<fib.Length; i++)
            Console.WriteLine(fib[i]);
        */

        /*
        TicTacToe.LoescheDasSpielfeld();
        TicTacToe.GibSpielbrettAus();
        */

        int[] zahlen = new int[100];
        Array.Fill(zahlen, 42);
        Array.Reverse(zahlen);
        int position = Array.IndexOf(zahlen, 42);
        Array.Sort(zahlen);
        Array.Clear(zahlen);
    }
}