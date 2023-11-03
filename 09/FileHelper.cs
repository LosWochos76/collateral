public class FileHelper
{
    public static char[] ReadAsChars(string filename)
    {
        string s = File.ReadAllText(filename);
        return s.ToArray();
    }

    public static Dictionary<char, int> GetFrequencies(char[] content)
    {
        var freq = new Dictionary<char, int>();
        for (int i=0; i<content.Length; i++)
            if (!freq.ContainsKey(content[i]))
                freq[content[i]] = 1;
            else
                freq[content[i]]++;

        return freq;
    }
}