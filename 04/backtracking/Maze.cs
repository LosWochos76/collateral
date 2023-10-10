public class Maze
{
    private int[,] maze;
    private int call_count = 0;
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
        Console.WriteLine(call_count);
        return min;
    }

    private void Solve(int x, int y, int current_sum)
    {
        current_sum += maze[y, x];
        current_path.Add(string.Format("{0},{1}",x,y));
        call_count++;

        var max_y = maze.GetLength(0) - 1;
        var max_x = maze.GetLength(1) - 1;

        if (current_sum > min)
        { 
            Console.WriteLine("BOUND! {0}: {1}", current_sum, string.Join("->", current_path.ToArray()));
        }
        else if (y == max_y && x == max_x)
        {
            if (current_sum <= min)
            {
                min = current_sum;
                best_path = string.Join("->", current_path.ToArray());
                Console.WriteLine("NEW BEST {0}: {1}", min, best_path);
            }
            else
            {
                Console.WriteLine("NOT BETTER {0}: {1}", min, best_path);
            }
        }
        else if (y == max_y)
        {
            Solve(x+1, y, current_sum);
        }
        else if (x == max_x)
        {
            Solve(x, y+1, current_sum);
        }
        else
        {
            Solve(x+1, y, current_sum);
            Solve(x, y+1, current_sum);
        }

        current_path.RemoveAt(current_path.Count-1);
    }
}