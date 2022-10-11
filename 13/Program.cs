using System.Net;
using System.Net.Sockets;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {  
        //CreateFiles();
        //CheckPngFile();

        /*Console.Write("Dies ist ein Smiley: ");
        WriteSmiley();
        Console.WriteLine();*/

        Console.WriteLine(IsValidPngFile("Wikipedia-logo-fixed.png"));
    }

    public static void FindFiles(string path, string ending) 
    {
        Console.WriteLine(path);
        ending = "." + ending.ToLower();

        foreach (var file in Directory.GetFiles(path))
        {
            if (file.ToLower().EndsWith(ending))
            Console.WriteLine(file);
        }
    }

    public static void CreateFiles() 
    {
        for (int ascii=65; ascii<=90; ascii++)
        {
            string filename = Convert.ToChar(ascii) + ".txt";
            File.WriteAllText(filename, filename);
        }
    }

    public static void WriteSmiley()
    {
        Stream s = Console.OpenStandardOutput();
        s.Write(new byte[] { 0xF0, 0x9F, 0x98, 0x81 }, 0, 4);
        s.Close();
    }

    public static bool IsValidPngFile(string filename)
    {
        FileStream stream = File.Open(filename, FileMode.Open);
        byte[] header = new byte[8];
        stream.Read(header, 0, 8);
        stream.Close();

        string header_as_string = BitConverter.ToString(header);
        return header_as_string == "89-50-4E-47-0D-0A-1A-0A";
    }
}