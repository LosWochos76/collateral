using System.Collections;

public class ArrayListEnumerator<T> : IEnumerator<T>
{
    private int current = -1;
    private ArrayList<T> list;

    public ArrayListEnumerator(ArrayList<T> list)
    {
        this.list = list;
    }

    public T Current 
    {
        get
        {
            if (current < 0 || current >= list.Count)
                throw new Exception("No current element!");

            return list[current];
        }
    }

    object IEnumerator.Current 
    {
        get
        {
            return Current;
        }
    }

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
        if (current == list.Count - 1)
            return false;
        
        current++;
        return true;
    }

    public void Reset()
    {
        current = -1;
    }
}