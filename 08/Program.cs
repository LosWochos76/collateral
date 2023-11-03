/*var list = new ArrayList<int>();
var rnd = new Random();
for (int i=0; i<10; i++)
    list.Add(rnd.Next(100));

list.SelectionSort();
list.Print();*/

/*var list = new LinkedList<int>();
var rnd = new Random();
for (int i=0; i<10; i++)
    list.Enqueue(rnd.Next(100));

list.SelectionSort();
list.Print();*/

/*var list = new ArrayList<int>();
var rnd = new Random();
for (int i=0; i<10; i++)
    list.Add(rnd.Next(100));

list.BubbleSort();
list.Print();*/

var rnd = new Random();
int[] zahlen = new int[10];
for (int i=0; i<10; i++)
    zahlen[i] = rnd.Next(100);

var sorted = MergeSort.Sort(zahlen);
for (int i=0; i<10; i++)
    Console.WriteLine(sorted[i]);