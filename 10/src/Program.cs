/*var hm = new HashMapWithChaining<char, int>(2);
Console.WriteLine(hm.LoadFactor);
hm.Insert('a', 100);
Console.WriteLine(hm.LoadFactor);
hm.Insert('b', 200);
Console.WriteLine(hm.LoadFactor);*/


string input = "geekific-geekific";
List<int> compressed = LZW.Compress(input);
string decompressed = LZW.Decompress(compressed);

Console.WriteLine("Original: " + input);
Console.WriteLine("Compressed: " + string.Join(", ", compressed));
Console.WriteLine("Decompressed: " + decompressed);