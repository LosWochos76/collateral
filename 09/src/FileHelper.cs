public class FileHelper
{
    public static Dictionary<char, int> GetFrequencies(string content)
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