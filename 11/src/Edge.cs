public class Edge : IComparable<Edge>
{
    public bool IsDirected { get; private set; }
    public bool IsUndirected { get { return !IsDirected; } }
    public int Vertex1 { get; private set; }
    public int Vertex2 { get; set; }
    public double Weight { get; private set; }

    public Edge(bool is_directed, int vertex1, int vertex2, double weight)
    {
        IsDirected = is_directed;
        Vertex1 = vertex1;
        Vertex2 = vertex2;
        Weight = weight;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Edge)
            return false;

        var other = obj as Edge;
        if (IsDirected && other.IsDirected)
            return Vertex1.Equals(other.Vertex1) && 
                Vertex2.Equals(other.Vertex2);
        
        if (IsUndirected && other.IsUndirected)
            return Vertex1.Equals(other.Vertex1) && 
                    Vertex2.Equals(other.Vertex2) ||
                Vertex1.Equals(other.Vertex2) && 
                    Vertex2.Equals(other.Vertex1);
        
        return false;
    }

    public int CompareTo(Edge? other)
    {
        return Weight.CompareTo(other.Weight);
    }

    public override string ToString()
    {
        return string.Format("{0}->{1}", Vertex1, Vertex2);
    }
}