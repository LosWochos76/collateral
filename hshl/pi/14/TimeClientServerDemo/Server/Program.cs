using System.Net;
using System.Net.Sockets;
using System.Text;

public class Program
{
    public static void Main()
    {
        Listen();
    }

    public static void Listen()
    {
        var adress = IPAddress.Parse("127.0.0.1");
        var listener = new TcpListener(adress, 12345);
        Console.WriteLine("Starting server...");
        listener.Start();

        while (true)
        {
            var client = listener.AcceptTcpClient();
            WriteTimeAsync(client);
        }
    }

    public static Task WriteTimeAsync(TcpClient client)
    {
        return Task.Factory.StartNew(() => { WriteTime(client); });
    }

    public static void WriteTime(TcpClient client)
    {
        using (var stream = client.GetStream())
        {
            do
            {
                Console.WriteLine("Writing time...");

                if (!Write(stream, DateTime.Now.ToString()))
                {
                    Console.WriteLine("Connection lost!");
                    return;
                }

                Thread.Sleep(1000);
            }
            while (true);
        }
    }

    private static bool Write(NetworkStream stream, string text)
    {
        var bytes = Encoding.ASCII.GetBytes(text);
        try
        {
            stream.Write(bytes, 0, bytes.Length);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }
}