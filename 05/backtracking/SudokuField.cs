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

    public bool IsNumberGoodInRow(int pos, int number)
    {
        int y = pos / 9;
        for (int x=0; x<9; x++)
            if (field[y, x] == number)
                return false;

        return true;
    }

    public bool IsNumberGoodInColumn(int pos, int number)
    {
        int x = pos % 9;
        for (int y=0; y<9; y++)
            if (field[y, x] == number)
                return false;

        return true;
    }

    public bool IsNumberGoodInBlock(int pos, int number)
    {
        var starty = (pos / 9) / 3 * 3;
        var startx = (pos % 9) / 3 * 3;

        int[] result = new int[9];
        for (int y=0; y<3; y++)
            for (int x=0; x<3; x++)
                if (field[starty + y, startx + x] == number)
                    return false;

        return true;
    }

    public bool Fill(int pos)
    {
        if (pos == 81)
        {
            Print();
            return true;
        }
        
        while (field[pos / 9, pos % 9] > 0)
        {
            pos++;
            if (pos == 81)
            {
                Print();
                return true;
            }
        }

        for (int number=1; number<=9; number++)
        {
            if (IsNumberGoodInRow(pos, number) && 
                IsNumberGoodInColumn(pos, number) &&
                IsNumberGoodInBlock(pos, number))
            {
                field[pos / 9, pos % 9] = number;
                if (Fill(pos + 1))
                    return true;
            }
        }

        field[pos / 9, pos % 9] = 0;
        return false;
    }
}