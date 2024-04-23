public class Program
{
    public static void Main()
    {
        PrintResult();
        Console.ReadLine();
    }

    public static async void PrintResult()
    {
        int result = await Return42();
        Console.WriteLine(result);
    }

    public static Task<int> Return42()
    {
        return Task.Factory.StartNew(() =>
        {
            Task.Delay(2000).Wait();
            return 42;
        });
    }
}