using System.Text;

public class Hufman
{
    public HufmanNode Root { get; private set; } = null;
    private Dictionary<char, string> codeTable;

    public Hufman(Dictionary<char, int> frequencies)
    {
        var heap = ToHeap(frequencies);
        Root = ToHufmannTree(heap);

        BuildCodeTable();
    }

    private static MaxHeap<HufmanNode> ToHeap(Dictionary<char, int> freq)
    {
        var heap = new MaxHeap<HufmanNode>();
        foreach (var kvp in freq)
            heap.Insert(new HufmanNode(kvp.Key, kvp.Value));

        return heap;
    }

    private static HufmanNode ToHufmannTree(MaxHeap<HufmanNode> heap)
    {
        while (heap.Count > 1)
        {
            var left = heap.ExtractMax();
            var right = heap.ExtractMax();

            var parent = new HufmanNode('\0', left.Freq + right.Freq);
            parent.Left = left;
            parent.Right = right;

            heap.Insert(parent);
        }

        return heap.ExtractMax();
    }

    private void BuildCodeTable()
    {
        codeTable = new Dictionary<char, string>();
        BuildCodeTable(Root, string.Empty);
    }

    private void BuildCodeTable(HufmanNode node, string code)
    {
        if (node.Character != '\0')
            codeTable[node.Character] = code;

        if (node.Left != null)
            BuildCodeTable(node.Left, code + "0");

        if (node.Right != null)
            BuildCodeTable(node.Right, code + "1");
    }

    public string Compress(string content)
    {
        StringBuilder builder = new StringBuilder();
        foreach (char c in content)
            builder.Append(codeTable[c]);
        
        return builder.ToString();
    }

    public string Decompress(string encodedText)
    {
        var decodedText = new StringBuilder();
        var currentNode = Root;

        foreach (char bit in encodedText)
        {
            if (bit == '0')
            {
                currentNode = currentNode.Left;
            }
            else if (bit == '1')
            {
                currentNode = currentNode.Right;
            }

            if (currentNode.Character != '\0')
            {
                decodedText.Append(currentNode.Character);
                currentNode = Root;
            }
        }

        return decodedText.ToString();
    }
}