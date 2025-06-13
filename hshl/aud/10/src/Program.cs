/*var hm = new HashMapWithChaining<char, int>(2);
Console.WriteLine(hm.LoadFactor);
hm.Insert('a', 100);
Console.WriteLine(hm.LoadFactor);
hm.Insert('b', 200);
Console.WriteLine(hm.LoadFactor);*/

string input = "ananas";
Console.WriteLine("Original: " + input);

var compress_dict = new Dictionary<string, int>();
compress_dict.Add("a", 0);
compress_dict.Add("n", 1);
compress_dict.Add("s", 2);
List<int> compressed = LZW.Compress(input, compress_dict);
Console.WriteLine("Compressed: " + string.Join(", ", compressed));

var decompress_dict = new Dictionary<int, string>();
decompress_dict.Add(0, "a");
decompress_dict.Add(1, "n");
decompress_dict.Add(2, "s");
string decompressed = LZW.Decompress(compressed, decompress_dict);
Console.WriteLine("Decompressed: " + decompressed);

decompress_dict = new Dictionary<int, string>();
decompress_dict.Add(0, "a");
decompress_dict.Add(1, "b");
decompress_dict.Add(2, "r");
decompress_dict.Add(3, "c");
decompress_dict.Add(4, "d");
compressed = new List<int>() { 0, 1, 2, 0, 3, 0, 4, 5, 7 };
decompressed = LZW.Decompress(compressed, decompress_dict);
Console.WriteLine("Decompressed: " + decompressed);
foreach (var item in decompress_dict)
    Console.WriteLine($"{item.Key} - {item.Value}");