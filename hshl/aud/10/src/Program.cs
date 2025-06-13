/*var hm = new HashMapWithChaining<char, int>(2);
Console.WriteLine(hm.LoadFactor);
hm.Insert('a', 100);
Console.WriteLine(hm.LoadFactor);
hm.Insert('b', 200);
Console.WriteLine(hm.LoadFactor);*/

string input = "ananas";
Console.WriteLine("Original: " + input);

var compressed = LZW.Compress(input, "a=0;n=1;s=2");
Console.WriteLine("Compressed: " + string.Join(", ", compressed));
string decompressed = LZW.Decompress(compressed, "0=a;1=n;2=s");
Console.WriteLine("Decompressed: " + decompressed);

compressed = new List<int>() { 0, 1, 2, 0, 3, 0, 4, 5, 7 };
decompressed = LZW.Decompress(compressed, "0=a;1=b;2=r;3=c;4=d");
Console.WriteLine("Decompressed: " + decompressed);