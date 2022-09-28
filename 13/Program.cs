public class Program
{
    public static void Main(string[] args)
    {  
        CreateFiles();
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
}