public class Edge : IComparable<Edge>
{
    public bool IsDirected { get; private set; }
    public bool IsUndirected { get { return !IsDirected; } }
    public int U { get; private set; }
    public int V { get; private set; }
    public double Weight { get; private set; }

    public Edge(bool is_directed, int u, int v, double weight = 1)
    {
        if (u == v)
            throw new Exception("Schleifen sind nicht erlaubt!");

        IsDirected = is_directed;
        U = u;
        V = v;
        Weight = weight;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Edge)
            return false;

        var other = obj as Edge;
        if (IsDirected && other.IsDirected)
            return U.Equals(other.U) && 
                V.Equals(other.V);
        
        if (IsUndirected && other.IsUndirected)
            return U.Equals(other.U) && 
                    V.Equals(other.V) ||
                U.Equals(other.V) && 
                    V.Equals(other.U);
        
        return false;
    }

    public override int GetHashCode()
    {
        if (IsDirected)
            return (U, V).GetHashCode();
        else
            return U.GetHashCode() ^ V.GetHashCode();
    }

    public int CompareTo(Edge? other)
    {
        return Weight.CompareTo(other.Weight);
    }

    public override string ToString()
    {
        if (IsDirected)
            return string.Format("{0}->{1}", U, V);
        else
            return string.Format("{0}<->{1}", U, V);
    }

    public bool ConnectsTo(Edge other)
    {
        if (IsDirected && other.IsDirected)
            if (V.Equals(other.U))
                return true;
        
        if (IsUndirected && other.IsUndirected)
            if (V.Equals(other.U) ||
                U.Equals(other.V))
                return true;
        
        return false;
    }

    public bool IsPartOf(int u)
    {
        return U.Equals(u) || V.Equals(u);
    }
}