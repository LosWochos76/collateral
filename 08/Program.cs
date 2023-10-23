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

var list = new ArrayList<int>();
var rnd = new Random();
for (int i=0; i<10; i++)
    list.Add(rnd.Next(100));

list.BubbleSort();
list.Print();