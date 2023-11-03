/*var array1 = ArrayHelper.RandomNumbers(10);
SelectionsortSorter.Sort(array1);
ArrayHelper.Print(array1);*/

/*var list = LinkedList<int>.RandomNumbers(10);
SelectionsortSorter.Sort(list);
list.Print();*/

/*var array2 = ArrayHelper.RandomNumbers(10);
BubblesortSorter.Sort(array2);
ArrayHelper.Print(array2);*/

/*var array = ArrayHelper.RandomNumbers(10);
QuicksortSorter.Sort(array);
ArrayHelper.Print(array);*/

var list = new ArrayList<int>();
var rnd = new Random();
for (int i=0; i<10; i++)
    list.Add(rnd.Next(100));

list.BubbleSort();
list.Print();