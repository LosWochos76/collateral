public class Block
{
    public static readonly int Breite = 5;
    public static readonly int Hoehe = 5;

    public int X { get; set; }
    public int Y { get; set; }

    public Block(int x, int y)
    {
        X = x;
        Y = y;
    }
}