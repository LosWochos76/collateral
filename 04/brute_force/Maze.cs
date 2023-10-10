public class Maze 
{
    public static int NumberOfPossiblePaths(int m, int n)
    {
        if (m == 1 || n == 1)
            return 1;
 
        return NumberOfPossiblePaths(m - 1, n) + 
            NumberOfPossiblePaths(m, n - 1);
    }

    private int[,] maze;
    private int paths = 0;
    private int min = int.MaxValue;
    private List<string> current_path = new List<string>();
    private string best_path;

    public Maze(int[,] maze)
    {
        this.maze = maze;
    }

    public int Solve()
    {
        Solve(0,0,0);
        Console.WriteLine("PATHS: {0}", paths);
        return min;
    }

    private void Solve(int x, int y, int current_sum)
    {
        var max_y = maze.GetLength(0) - 1;
        var max_x = maze.GetLength(1) - 1;

        if (x > max_x || y > max_y)
            return;

        current_sum += maze[y, x];
        current_path.Add(string.Format("{0},{1}",x,y));

        if (y == max_y && x == max_x)
        {
            Console.WriteLine("FOUND PATH: {0}", string.Join("->", current_path.ToArray()));
            min = current_sum <= min ? current_sum : min;
            paths++;
        }
        else
        {
            Solve(x+1, y, current_sum);
            Solve(x, y+1, current_sum);
        }

        current_path.RemoveAt(current_path.Count-1);
    }
}