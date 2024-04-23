using System;

public class Program
{
    public static int WuerfeBisZweimal6Zaehlen()
    {
        Random rnd = new Random();
        int counter = 0;

        while (true)
        {
            counter++;
            int w1 = rnd.Next(1, 7);
            int w2 = rnd.Next(1, 7);

            Console.WriteLine("Würfel1={0}, Würfel2={0}.", w1, w2);
            if (w1 == 6 && w2 == 6)
                return counter;
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine(
            "Es musste {0} mal gewürfelt werden, um 2 mal die 6 zu erhalten!", 
            WuerfeBisZweimal6Zaehlen());
    }
}
