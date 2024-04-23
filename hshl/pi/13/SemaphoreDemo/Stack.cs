public class Stack
{
    private int[] memory;
    private int pos = -1;
    private Semaphore free;
    private Semaphore taken;

    public Stack(int size)
    {
        memory = new int[size];
        free = new Semaphore(size, size);
        taken = new Semaphore(0, size);
    }

    public int Count
    {
      get { return pos+1; }
    }

    public void Push(int value)
    {
        free.WaitOne();
        pos++;
        memory[pos] = value;
        taken.Release();
    }

    public int Pop()
    {
        taken.WaitOne();
        var value = memory[pos];
        pos--;
        free.Release();
        return value;
    }
}