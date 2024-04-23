public class ArrayHelper
{
    public static void Swap(int[] array, int index1, int index2)
    {
        if (index1 == index2)
            return;

        var temp = array[index1];
        array[index1] = array[index2];
        array[index2] = temp;
    }

    public static int[] RandomNumbers(int count)
    {
        var rnd = new Random();
        var list = new int[count];

        for (int i=0; i<count; i++)
            list[i] = rnd.Next(100);
        
        return list;
    }

    public static void Print(int[] array)
    {
        for (int i=0; i<array.Length; i++)
            Console.WriteLine("{0}: {1}", i, array[i]);
    }

    public static bool IsSorted(int[] array)
    {
        if (array.Length <= 1)
            return true;

        for (int i=1; i<array.Length; i++)
            if (array[i-1] > array[i])
                return false;
        
        return true;
    }
}