using System.Text;

public class LZW
{
    private static Dictionary<string, int> GetInitialCompressionDictionary()
    {
        var dictionary = new Dictionary<string, int>();
        for (int i = 0; i < 256; i++)
            dictionary.Add(Convert.ToChar(i).ToString(), i);
        
        return dictionary;
    }

    public static List<int> Compress(string uncompressed)
    {
        var dictionary = GetInitialCompressionDictionary();
        return Compress(uncompressed, dictionary);
    }

    public static List<int> Compress(string uncompressed, string dictionary)
    {
        var dict = new Dictionary<string, int>();
        foreach (string element in dictionary.Split(';'))
        {
            var parts = element.Split('=');
            var key = parts[0];
            var value = Convert.ToInt32(parts[1]);
            dict.Add(key, value);
        }
        return Compress(uncompressed, dict);
    }

    public static List<int> Compress(string uncompressed, Dictionary<string, int> dictionary)
    {
        string last_word = uncompressed[0].ToString();
        List<int> compressed = new List<int>();
        
        for (int i=1; i<uncompressed.Length; i++)
        {
            char current_char = uncompressed[i];
            string current_word = last_word + current_char;

            if (dictionary.ContainsKey(current_word))
            {
                last_word = current_word;
            }
            else
            {
                compressed.Add(dictionary[last_word]);
                dictionary.Add(current_word, dictionary.Count);
                last_word = current_char.ToString();
            }
        }

        if (!string.IsNullOrEmpty(last_word))
            compressed.Add(dictionary[last_word]);

        return compressed;
    }

    private static Dictionary<int, string> GetInitialDecompressionDictionary()
    {
        var dictionary = new Dictionary<int, string>();
        for (int i = 0; i < 256; i++)
            dictionary.Add(i, Convert.ToChar(i).ToString());
        
        return dictionary;
    }

    public static string Decompress(List<int> compressed)
    {
        var dictionary = GetInitialDecompressionDictionary();
        return Decompress(compressed, dictionary);
    }

    public static string Decompress(List<int> compressed, string dictionary)
    {
        var dict = new Dictionary<int, string>();
        foreach (string element in dictionary.Split(';'))
        {
            var parts = element.Split('=');
            var key = Convert.ToInt32(parts[0]);
            var value = parts[1];
            dict.Add(key, value);
        }
        return Decompress(compressed, dict);
    }
    
    public static string Decompress(List<int> compressed, Dictionary<int, string> dictionary)
    {
        string last_word = dictionary[compressed[0]];
        StringBuilder result = new StringBuilder(last_word);

        for (int i = 1; i < compressed.Count; i++)
        {
            int code = compressed[i];
            string current_word = string.Empty;

            if (dictionary.ContainsKey(code))
                current_word = dictionary[code];
            else if (code == dictionary.Count)
                current_word = last_word + last_word[0];

            result.Append(current_word);
            var new_word = last_word + current_word[0];
            dictionary.Add(dictionary.Count, new_word);
            last_word = current_word;
        }

        return result.ToString();
    }
}