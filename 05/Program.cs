using System.Collections;

static void TestArrayList()
{
    var rnd = new Random();
    ArrayList<int> list = new ArrayList<int>();
    for (int i=0; i<1000; i++)
        list.Add(rnd.Next());

    list.Print();
}

static void TestLinkedList()
{
    var rnd = new Random();
    LinkedList<int> list = new LinkedList<int>();
    for (int i=0; i<1000; i++)
        list.Add(rnd.Next());

    while (list.HasElements)
        Console.WriteLine(list.Remove());
}

TestLinkedList();