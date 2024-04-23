using System.Net.Sockets;
using System.Text;

public class Program
{
    public static void Main()
    {
        ReadTime();

        Console.ReadLine();
    }

    public static void ReadTime()
    {
        using (var client = new TcpClient("127.0.0.1", 12345))
        {
            Console.WriteLine("Connected...");
            ReadTime(client);
        }
    }

    public static async void ReadTime(TcpClient client)
    {
        using (var stream = client.GetStream())
        {
            while (true)
            {
                var s = ReadString(stream);
                Console.WriteLine(s);
            }
        }
    }

    private static string ReadString(NetworkStream stream)
    {
        var buffer = new byte[1024];
        int count = stream.Read(buffer, 0, buffer.Length);
        return Encoding.ASCII.GetString(buffer, 0, count);
    }
}