using System.Text;

class LZW
{
    public static List<int> Compress(string uncompressed)
    {
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        for (int i = 0; i < 256; i++)
            dictionary.Add(((char)i).ToString(), i);

        string w = string.Empty;
        List<int> compressed = new List<int>();

        foreach (char c in uncompressed)
        {
            string wc = w + c;
            if (dictionary.ContainsKey(wc))
            {
                w = wc;
            }
            else
            {
                compressed.Add(dictionary[w]);
                dictionary.Add(wc, dictionary.Count);
                w = c.ToString();
            }
        }

        if (!string.IsNullOrEmpty(w))
            compressed.Add(dictionary[w]);

        return compressed;
    }

    public static string Decompress(List<int> compressed)
    {
        Dictionary<int, string> dictionary = new Dictionary<int, string>();
        for (int i = 0; i < 256; i++)
            dictionary.Add(i, ((char)i).ToString());

        string current = dictionary[compressed[0]];
        StringBuilder result = new StringBuilder(current);
        for (int i = 1; i < compressed.Count; i++)
        {
            string symbol;
            if (dictionary.ContainsKey(compressed[i]))
                symbol = dictionary[compressed[i]];
            else if (compressed[i] == dictionary.Count)
                symbol = current + current[0];
            else
                throw new Exception("Fehler beim Dekomprimieren.");

            result.Append(symbol);
            dictionary.Add(dictionary.Count, current + symbol[0]);
            current = symbol;
        }

        return result.ToString();
    }
}