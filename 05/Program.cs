static void TestArrayList()
{
    var rnd = new Random();
    ArrayList<int> list = new ArrayList<int>();
    for (int i=0; i<1000; i++)
        list.Add(rnd.Next());

    list.Print();
}

static void TestStack()
{
    LinkedList<int> list = new LinkedList<int>();
    for (int i=0; i<10; i++)
        list.Push(i);

    while (list.HasElements)
        Console.WriteLine(list.Pop());
}

static void TestQueue()
{
    LinkedList<int> list = new LinkedList<int>();
    for (int i=0; i<10; i++)
        list.Enqueue(i);

    while (list.HasElements)
        Console.WriteLine(list.Dequeue());
}

TestQueue();