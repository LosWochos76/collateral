using System;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GraphSearch;

public partial class Block : ObservableObject
{
    [ObservableProperty]
    
    private int xPos;

    public int X { get { return XPos * 23; } }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Y))]
    private int yPos;
    
    public int Y { get { return YPos * 23; } }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Background))]
    private Color color;
    
    public SolidColorBrush Background
    {
        get { return new SolidColorBrush(Color); }
    }

    public Block(int xPos, int yPos)
    {
        XPos = xPos;
        YPos = yPos;
        Color = Colors.White;
    }

    public bool IsNeighborOf(Block b)
    {
        if (b == this)
            return false;

        if (Color == Colors.Black)
            return false;
        
        if (b.Color == Colors.Black)
            return false;
        
        return Math.Abs(XPos - b.XPos) <= 1 
               && Math.Abs(YPos - b.YPos) <= 1;
    }

    public double GetDistance(Block b)
    {
        return Math.Sqrt(Math.Pow(X - b.X, 2) + Math.Pow(Y - b.Y, 2));
    }
}