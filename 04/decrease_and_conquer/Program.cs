var zahlen = ArrayHelper.Zufallszahlen(1000);
Console.WriteLine(ArrayHelper.IsSorted(zahlen));
ArrayHelper.SelectionSort(zahlen);
Console.WriteLine(ArrayHelper.IsSorted(zahlen));
