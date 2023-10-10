var fields = SudokuFieldReader.ReadFromFile("sudoku.txt");

int sum = 0;
foreach (var f in fields)
    if (f.Solve())
        sum += f.GetTopLeftNumber();

Console.WriteLine(sum);