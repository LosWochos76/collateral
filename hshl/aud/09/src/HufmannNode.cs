public class HufmanNode : IComparable<HufmanNode>
{
    public char Character { get; set; }
    public int Freq { get; set; }
    public HufmanNode Left { get; set; }
    public HufmanNode Right { get; set; }
    public bool IsLeaf { get { return Left == null && Right == null; } }
    public bool HasLeftBranch { get { return Left != null; } }
    public bool HasRightBranch { get { return Right != null; } }

    public HufmanNode(char c, int f)
    {
        Character = c;
        Freq = f;
    }

    public int CompareTo(HufmanNode? other)
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