/*var fields = SudokuFieldReader.ReadFromFile("sudoku.txt");

int sum = 0;
foreach (var f in fields)
    if (f.Solve())
        sum += f.GetTopLeftNumber();

Console.WriteLine(sum);*/

var maze = new Maze(new int[,] {
    {  0,  3, 10,  3,  1,  2 },
    { 15,  7, 17, 11,  3,  5 },
    { 21,  9,  4,  2,  1,  2 },
    {  3,  7,  6,  3,  5,  0 } });

Console.WriteLine(maze.Solve());