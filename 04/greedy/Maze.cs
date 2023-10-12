public class Maze
{
    private int[,] maze;

    public Maze(int[,] maze)
    {
        this.maze = maze;
    }

    public void Solve()
    {
        Solve(0,0,0);
    }

    private void Solve(int x, int y, int current_sum)
    {
        var max_y = maze.GetLength(0) - 1;
        var max_x = maze.GetLength(1) - 1;

        if (x > max_x || y > max_y)
            return;

        Console.WriteLine("{0},{1}:{2}", x, y, maze[y,x]);
        current_sum += maze[y,x];
        if (x == max_x && y == max_y)
            Console.WriteLine(current_sum);
        
        if (x+1 > max_x || maze[y,x+1] > maze[y+1,x])
            Solve(x, y+1, current_sum);
        else if (y+1 > max_y || maze[y,x+1] <= maze[y+1,x])
            Solve(x+1, y, current_sum);
    }
}