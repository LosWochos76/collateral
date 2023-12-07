public class Path
{
    private List<Edge> edges;

    public Path()
    {
       edges = new List<Edge>();
    }

    public void Push(Edge e) 
    { 
        edges.Add(e); 
    }

    public Edge Pop() 
    { 
        var edge = edges.Last();
        edges.RemoveAt(edges.Count - 1);
        return edge;
    }

    public int Length { get { return edges.Count; } }

    public bool IsInPath(Edge edge)
    {
        foreach (var e in edges)
            if (edge.Equals(e))
                return true;
        
        return false;
    }

    public override string ToString()
    {
        return string.Join(",", Array.ConvertAll(edges.ToArray(), x => x.ToString()));
    }

    public void Print()
    {
        Console.WriteLine(this);
    }

    public void Clear()
    {
        edges = new List<Edge>();
    }
}