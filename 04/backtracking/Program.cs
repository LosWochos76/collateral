var fields = SudokuFieldReader.ReadFromFile("sudoku.txt");

foreach (var f in fields)
    f.Fill(0);