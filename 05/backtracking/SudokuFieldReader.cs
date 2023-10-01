public class SudokuFieldReader
{
    public static IEnumerable<SudokuField> ReadFromFile(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var fields = new List<SudokuField>();

        SudokuField current = new SudokuField();
        foreach (var line in lines)
        {
            if (line.StartsWith("Grid"))
            {
                current = new SudokuField();
                fields.Add(current);
            }
            else
            {
                current.AddLine(line);
            }
        }

        return fields;
    }
}