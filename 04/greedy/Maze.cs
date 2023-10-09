public class Maze
{
    private int[,] maze;

    public Maze(int[,] maze)
    {
        this.maze = maze;
    }

    public int SolveGreedy()
    {
        return SolveGreedy(0,0);
    }

    private int SolveGreedy(int x, int y)
    {
        Console.WriteLine("{0},{1},{2}", x, y, maze[x,y]);

        if (x == maze.GetLength(0)-1 || y == maze.GetLength(1) - 1)
            return maze[x,y];
        
        if (maze[x+1,y] < maze[x,y+1])
            return maze[x,y] + SolveGreedy(x+1, y);
        else
            return maze[x,y] + SolveGreedy(x,y+1);
    }
}