public interface IWeightedGraph
{
    void AddEdge(int u, int v, double weight);
    bool HasEdge(int u, int v);
    double GetWeight(int u, int v);
    void DeleteEdge(int u, int v);
    IEnumerable<int> GetNeighborsOf(int u);
    IEnumerable<int> AllNodes { get; }
    public int NodeCount { get; }
    public int EdgeCount { get; }
}