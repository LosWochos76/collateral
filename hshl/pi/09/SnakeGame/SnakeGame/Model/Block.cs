namespace SnakeGame.Model;

public class Block
{
    public int X { get; set; }
    public int Y { get; set; }

    public Block(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool IsInvalid
    {
        get
        {
            return X < 0 || X > 790 || Y < 0 || Y > 590;
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        var block = obj as Block;
        if (block == null) return false;
        if (block.X == X && block.Y == Y) return true;
        return false;
    }
}
