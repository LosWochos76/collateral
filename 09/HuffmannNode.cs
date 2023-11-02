public class HuffmanNode : IComparable<HuffmanNode>
{
    public char Character { get; set; }
    public int Freq { get; set; }
    public HuffmanNode Left { get; set; }
    public HuffmanNode Right { get; set; }

    public HuffmanNode(char c, int f)
    {
        Character = c;
        Freq = f;
    }

    public int CompareTo(HuffmanNode? other)
    {
        if (other == null)
            return -1;
        
        if (Freq == other.Freq)
            return 0;
        else if (Freq < other.Freq)
            return 1;
        else
            return -1;
    }
}