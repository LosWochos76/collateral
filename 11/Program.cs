public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 1)
            Console.WriteLine(Caesar.EncryptString(args[0]));
    }
}