abstract class GrafischesElement
{
    public int X { get; set; }
    public int Y { get; set; }

    public void Bewege(int delta_x, int delta_y)
    {
        this.X += delta_x;
        this.Y += delta_y;
    }

    public abstract void Zeichne();
}