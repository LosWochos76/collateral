public class UndirectedPath
{
    private List<int> path = new List<int>();

    public void PushNode(int u) 
    { 
        path.Add(u); 
    }

    public void PopNode() 
    { 
        path.RemoveAt(path.Count - 1); 
    }

    public int Count { get { return path.Count; } }

    public bool IsEdgeInPath(int u, int v)
    {
        for (int i=1; i<path.Count; i++)
            if (path[i] == u && path[i-1] == v || 
                path[i] == v && path[i-1] == u)
                    return true;
        
        return false;
    }

    public void Print()
    {
        Console.WriteLine(string.Join("->", Array.ConvertAll(path.ToArray(), x => x.ToString())));
    }
}