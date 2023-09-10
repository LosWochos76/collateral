public class Program
{
    public static void RunAnonymous()
    {
        var t1 = new Thread(() =>
        {
            while (true)
            {
                Console.WriteLine("Hello");
                Thread.Sleep(500);
            }
        });

        t1.Start();
        Console.WriteLine("Thread running!");
    }

    public static void Main(string[] args)
    {
        RunAnonymous();
    }
}