using System.Collections.Generic;
using System.Threading;
using Avalonia.Media;

namespace GraphSearch;

public class BreadthFirstSearch : IAlgorithm
{
    private MainWindowViewModel graph;
    private bool isRunning = true;
    private Dictionary<Block, Block> parent = new();
    
    public BreadthFirstSearch(MainWindowViewModel graph)
    {
        this.graph = graph;
    }

    public void Run()
    {
        var thread = new Thread(BackgroundWorder);
        thread.Start();
    }

    public void Stop()
    {
        isRunning = false;
        Thread.Sleep(100);
    }

    private void BackgroundWorder()
    {
        var queue = new Queue<Block>();
        queue.Enqueue(graph.Start);

        while (queue.Count > 0 && isRunning)
        {
            var b = queue.Dequeue();
            foreach (var n in graph.GetNeighborsOf(b))
            {
                if (n.Color == Colors.White || n.Color == Colors.Green)
                {
                    n.Color = n.Color != Colors.White? n.Color : Colors.Gray;
                    queue.Enqueue(n);
                    parent[n] = b;

                    if (n == graph.Destination)
                    {
                        Found();
                        return;
                    }

                    Thread.Sleep(5);
                }
            }
            
            b.Color = b.Color != Colors.Gray? b.Color : Colors.DarkGray;
        }
    }

    private void Found()
    {
        var b = graph.Destination;
        while (b != graph.Start)
        {
            b.Color = Colors.Blue;
            b = parent[b];
        }
    }
}