using System.Collections.Generic;
using System.Threading;
using Avalonia.Media;

namespace GraphSearch;

public class AStar : IAlgorithm
{
    private MainWindowViewModel graph;
    private bool isRunning = true;
    private Dictionary<Block, Block> parent = new();
    private List<Block> queue;
    
    public AStar(MainWindowViewModel graph)
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

    private Block Dequeue()
    {
        double min_dist = queue[0].GetDistance(graph.Destination);
        Block min = queue[0];

        if (queue.Count == 1)
            return min;

        for (int i = 1; i < queue.Count; i++)
        {
            double dist = queue[i].GetDistance(graph.Destination);
            if (dist < min_dist)
            {
                min_dist = dist;
                min = queue[i];
            }
        }

        queue.Remove(min);
        return min;
    }

    private void BackgroundWorder()
    {
        queue = new List<Block>();
        queue.Insert(0, graph.Start);

        while (queue.Count > 0 && isRunning)
        {
            var b = Dequeue();
            foreach (var n in graph.GetNeighborsOf(b))
            {
                if (n.Color == Colors.White || n.Color == Colors.Green)
                {
                    n.Color = n.Color != Colors.White? n.Color : Colors.Gray;
                    queue.Insert(0, n);
                    parent[n] = b;

                    if (n == graph.Destination)
                    {
                        Found();
                        return;
                    }

                    Thread.Sleep(10);
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