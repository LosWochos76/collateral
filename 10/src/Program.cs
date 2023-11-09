var hm = new HashMap<char, int>(2);
Console.WriteLine(hm.LoadFactor);
hm.Insert('a', 100);
Console.WriteLine(hm.LoadFactor);
hm.Insert('b', 200);
Console.WriteLine(hm.LoadFactor);