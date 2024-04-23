public class SudokuField
{
    public int current_line = 0;
    public int[,] field = new int[9,9];

    public void AddLine(string line)
    {
        for (int i=0; i<9; i++)
            field[current_line, i] = Convert.ToInt32(line[i]) - 48;
        
        current_line++;
    }

    public void Print()
    {
        for (int y=0; y<9; y++)
        {
            for (int x=0; x<9; x++)
                Console.Write("{0} ", field[y, x]);

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    public bool IsNumberGoodInRow(int y, int number)
    {
        for (int x=0; x<9; x++)
            if (field[y, x] == number)
                return false;

        return true;
    }

    public bool IsNumberGoodInColumn(int x, int number)
    {
        for (int y=0; y<9; y++)
            if (field[y, x] == number)
                return false;

        return true;
    }

    public bool IsNumberGoodInBlock(int x, int y, int number)
    {
        var starty = y / 3 * 3;
        var startx = x / 3 * 3;

        int[] result = new int[9];
        for (int y1=0; y1<3; y1++)
            for (int x1=0; x1<3; x1++)
                if (field[starty + y1, startx + x1] == number)
                    return false;

        return true;
    }

    public bool IsNumberGood(int x, int y, int number)
    {
        return IsNumberGoodInRow(y, number) && 
            IsNumberGoodInColumn(x, number) &&
            IsNumberGoodInBlock(x, y, number);
    }

    public bool Solve()
    {
        return Solve(0, 0);
    }

    private bool Solve(int x, int y)
    {
        y += x / 9;
        x %= 9;
        if (y > 8)
            return true;
        
        while (field[y, x] > 0)
            return Solve(x + 1, y);

        for (int number=1; number<=9; number++)
        {
            if (IsNumberGood(x, y, number))
            {
                field[y, x] = number;
                if (Solve(x + 1, y))
                    return true;
            }
        }

        field[y, x] = 0;
        return false;
    }

    public int GetTopLeftNumber()
    {
        return field[0,0]*100 + field[0,1] * 10 + field[0,2];
    }
}