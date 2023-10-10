var maze = new Maze(new int[,] {
    {  0,  3, 10,  3,  1,  2 },
    { 15,  7, 17, 11,  3,  5 },
    { 21,  9,  4,  2,  1,  2 },
    {  3,  7,  6,  3,  5,  0 } });

Console.WriteLine(maze.Solve());