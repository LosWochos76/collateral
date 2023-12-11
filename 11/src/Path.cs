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

    public bool IsPartOf(Edge edge)
    {
        return edges.Contains(edge);
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

    public IEnumerable<Edge> Edges
    {
        get { return edges; }
    }

    public Edge First { get { return edges[0]; } }
    public Edge Last { get { return edges[edges.Count - 1]; } }

    public bool IsConnected
    {
        get
        {
            for (int i=1; i<edges.Count; i++)
                if (!edges[i-1].ConnectsTo(edges[i]))
                    return false;

            return true;
        }
    }

    public bool IsCircle
    {
        get
        {
            return IsConnected && Last.ConnectsTo(First);
        }
    }
}