public class Maze
{
    private int[,] maze;

    public Maze(int[,] maze)
    {
        this.maze = maze;
    }

    public int Solve()
    {
        int[,] new_maze = new int[maze.GetLength(0), maze.GetLength(1)];
        for (int x=1; x<maze.GetLength(1); x++)
            new_maze[0, x] = maze[0, x] + new_maze[0, x - 1];

        for (int y=1; y<maze.GetLength(0); y++)
            new_maze[y, 0] = maze[y, 0] + new_maze[y - 1, 0];

        for (int y=1; y<new_maze.GetLength(0); y++)
            for (int x=1; x<new_maze.GetLength(1); x++)
                new_maze[y, x] = maze[y, x] + Math.Min(new_maze[y - 1, x], new_maze[y, x-1]);

        for (int y=0; y<new_maze.GetLength(0); y++)
        {
            for (int x=0; x<new_maze.GetLength(1); x++)
                Console.Write("{0} ", new_maze[y, x]);

            Console.WriteLine();
        }
 
        return new_maze[maze.GetLength(0)-1, maze.GetLength(1)-1];
    }
}