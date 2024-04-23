public class LinkedListHelper
{
    public static void SwapValues<T>(NodeElement<T> a, NodeElement<T> b)
    {
        var temp = a.Value;
        a.Value = b.Value;
        b.Value = temp;
    }

    public static LinkedList<int> RandomNumbers(int count)
    {
        var rnd = new Random();
        var list = new LinkedList<int>();
        for (int i=0; i<count; i++)
            list.Enqueue(rnd.Next(100));
        
        return list;
    }

    public static bool IsSorted(LinkedList<int> list)
    {
        var last = int.MinValue;
        var current = list.Head;

        while (current != null)
        {
            if (current.Value < last)
                return false;
            
            current = current.Next;
        }

        return true;
    }
}