public class Program
{
    public static Stack stack = new Stack(10);
    public static Random rnd = new Random();

    static void Main()
    {
        for (int i = 0; i < 100; i++)
            new Thread(Producer).Start();

        for (int i = 0; i < 10; i++)
            new Thread(Consumer).Start();

        Console.ReadLine();
    }

    private static void Producer()
    {
        while (true)
        {
            stack.Push(rnd.Next(0, 1000));
            Thread.Sleep(rnd.Next(0, 500));
        }
    }

    private static void Consumer()
    {
        while (true)
        {
            stack.Pop();
            Thread.Sleep(rnd.Next(0, 500));
        }
    }
}