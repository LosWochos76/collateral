public class PrimAlgorithm
{
    private IWeightedGraph graph;

    public PrimAlgorithm(IWeightedGraph graph)
    {
        this.graph = graph;
    }

    // Funktion zum Finden des minimalen Schlüsselwertes
    private int MinKey(Dictionary<int, double> key, Dictionary<int, bool> mstSet)
    {
        double min = double.MaxValue;
        int minIndex = -1;

        foreach (var v in key.Keys)
        {
            if (mstSet[v] == false && key[v] < min)
            {
                min = key[v];
                minIndex = v;
            }
        }

        return minIndex;
    }

    // Funktion zum Anzeigen des MST, der mit dem Prim-Algorithmus erstellt wurde
    private void PrintMST(Dictionary<int, int> parent, IWeightedGraph graph)
    {
        Console.WriteLine("Kante Gewicht");
        foreach (var node in graph.AllNodes)
        {
            Console.WriteLine(parent[node] + " - " + node + " " + graph.GetWeight(node, parent[node]));
        }
    }

    // Funktion zum Implementieren des Prim-Algorithmus für einen gegebenen gewichteten Graphen
    // mit der Graph-Klasse
    public void PrimMST()
    {
        var parent = new Dictionary<int, int>(); // Array zum Speichern des erstellten MST
        var key = new Dictionary<int, double>(); // Schlüsselwerte, die die minimalen Gewichte darstellen, um den Knoten zu erreichen
        var mstSet = new Dictionary<int, bool>(); // Array, um anzuzeigen, ob ein Knoten bereits im MST enthalten ist oder nicht

        // Initialisierung aller Schlüsselwerte als "unendlich" und mstSet[] als "false"
        foreach (var node in graph.AllNodes)
        {
            key[node] = double.MaxValue;
            mstSet[node] = false;
        }

        // Der erste Knoten wird als Wurzel ausgewählt, um den MST zu erstellen
        var first_node = graph.AllNodes.First();
        key[first_node] = 0;
        parent[first_node] = -1; // Der erste Knoten hat keinen Elternknoten

        // MST erstellen
        foreach (var node in graph.AllNodes)
        {
            // Den minimalen Schlüsselknoten auswählen, der noch nicht im MST enthalten ist
            int u = MinKey(key, mstSet);

            // Den ausgewählten Knoten im MST markieren
            mstSet[u] = true;

            // Aktualisiere den Schlüsselwert und den Elternindex der benachbarten Knoten des ausgewählten Knotens.
            foreach (int v in graph.GetNeighborsOf(u))
            {
                if (graph.HasEdge(u, v) && mstSet[v] == false && graph.GetWeight(u, v) < key[v])
                {
                    parent[v] = u;
                    key[v] = graph.GetWeight(u, v);
                }
            }
        }

        // MST anzeigen
        PrintMST(parent, graph);
    }
}