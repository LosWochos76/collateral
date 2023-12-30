public class DisjointSet
{
    private Dictionary<int, HashSet<int>> sets = new Dictionary<int, HashSet<int>>();

    public void MakeSet(int i)
    {
        sets[i] = new HashSet<int>();
        sets[i].Add(i);
    }

    public int FindSet(int i)
    {
        foreach (var k in sets.Keys)
            if (sets[k].Contains(i))
                return k;
        
        throw new Exception("Not found!");
    }

    public void Union(int u, int v)
    {
        u = FindSet(u);
        v = FindSet(v);
        sets[u].UnionWith(sets[v]);
        sets.Remove(v);
    }

    public bool AreInSameSet(int u, int v)
    {
        return FindSet(u) == FindSet(v);
    }

    public int Count { get { return sets.Count; } }
}