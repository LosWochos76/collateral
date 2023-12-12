public class Edge : IComparable<Edge>
{
    public bool IsDirected { get; private set; }
    public bool IsUndirected { get { return !IsDirected; } }
    public int Vertex1 { get; private set; }
    public int Vertex2 { get; set; }
    public double Weight { get; private set; }

    public Edge(bool is_directed, int vertex1, int vertex2, double weight = 1)
    {
        if (vertex1 == vertex2)
            throw new Exception("Schleifen sind nicht erlaubt!");

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

    public override int GetHashCode()
    {
        if (IsDirected)
            return (Vertex1, Vertex2).GetHashCode();
        else
            return Vertex1.GetHashCode() ^ Vertex2.GetHashCode();
    }

    public int CompareTo(Edge? other)
    {
        return Weight.CompareTo(other.Weight);
    }

    public override string ToString()
    {
        if (IsDirected)
            return string.Format("{0}->{1}", Vertex1, Vertex2);
        else
            return string.Format("{0}<->{1}", Vertex1, Vertex2);
    }

    public bool ConnectsTo(Edge other)
    {
        if (IsDirected && other.IsDirected)
            if (Vertex2.Equals(other.Vertex1))
                return true;
        
        if (IsUndirected && other.IsUndirected)
            if (Vertex2.Equals(other.Vertex1) ||
                Vertex1.Equals(other.Vertex2))
                return true;
        
        return false;
    }
}