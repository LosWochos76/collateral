var rnd = new Random();
ArrayList<int> ints = new ArrayList<int>();
for (int i=0; i<1000; i++)
    ints.Add(rnd.Next());

foreach (var e in ints)
    Console.WriteLine(e);