public class Program
{
    public static void FibonacciTest()
    {
        int fn = 0;
        while (fn < 10000)
        {
            fn = Fibonacci.Next();
            Console.WriteLine(fn);
        }
    }

    public static void RechteckTest()
    {
        Console.WriteLine("Umfang={0:F2} cm.", Rechteck.BerechneUmfang(5, 10));
        Console.WriteLine("Fläche={0:F2} cm^2.", Rechteck.BerechneFlaeche(5, 10));
    }

    public static void Main(string[] args)
    {
        //FibonacciTest();
        //RechteckTest();
    }
}