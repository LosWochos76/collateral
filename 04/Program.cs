using System.Collections;

static void TestArrayList()
{
    var rnd = new Random();

    ArrayList<int> ints = new ArrayList<int>();
    for (int i=0; i<1000; i++)
        ints.Add(rnd.Next());

    foreach (var e in ints)
        Console.WriteLine(e);
}

static void TestLinkedList()
{
    var rnd = new Random();

    LinkedList<int> ints = new LinkedList<int>();
    for (int i=0; i<1000; i++)
        ints.Add(rnd.Next());

    foreach (var e in ints)
        Console.WriteLine(e);
}

TestLinkedList();