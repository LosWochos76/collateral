using System.Collections.ObjectModel;
using System;

namespace SnakeGame.Model;

public enum Direction
{
    Right, Down, Left, Up
}

public class Snake
{
    private Game game;
    private Direction direction;
    private bool extend_next_move = false;
    public ObservableCollection<Block> Body { get; set; } = new ObservableCollection<Block>();

    public Snake(Game game)
    {
        this.game = game;
        direction = Direction.Right;
        Body.Add(new Block(120, 100));
        Body.Add(new Block(110, 100));
        Body.Add(new Block(100, 100));
    }

    public Block Head
    {
        get { return Body[0]; }
    }

    public void Move()
    {
        var new_block = NextBlock();

        if (new_block.IsInvalid)
            throw new Exception("Crash with wall!");

        if (IsBodyCrash)
            throw new Exception("Crash with body!");

        Body.Insert(0, new_block);

        if (extend_next_move)
            extend_next_move = false;
        else
            Body.RemoveAt(Body.Count - 1);
    }

    private bool IsBodyCrash
    {
        get
        {
            for (int i=1; i<Body.Count; i++) 
            {
                if (Body[i].Equals(Head))
                    return true;
            }

            return false;
        }
    }

    public void ExtendBody()
    {
        extend_next_move = true;
    }

    private Block NextBlock()
    {
        switch (direction)
        {
            case Direction.Left: return new Block(Head.X - 10, Head.Y);
            case Direction.Right: return new Block(Head.X + 10, Head.Y);
            case Direction.Up: return new Block(Head.X, Head.Y - 10);
            case Direction.Down: return new Block(Head.X, Head.Y + 10);
            default: return null;
        }
    }

    public void Turn(Direction direction)
    {
        this.direction = direction;
    }
}