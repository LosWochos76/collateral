using System;

class TicTacToe
{
    public static char[,] Zufallsfeld()
    {
        char[,] feld = new char[3, 3];
        Random rnd = new Random();

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                switch (rnd.Next(0, 3))
                {
                    case 1: feld[x, y] = 'X'; break;
                    case 2: feld[x, y] = 'O'; break;
                    default: feld[x, y] = ' '; break;
                }
            }
        }

        return feld;
    }

    public static void Ausgeben(char[,] feld)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write("'" + feld[i, j] + "' ");
            }

            Console.WriteLine();
        }
    }

    public static bool Gewonnen(char[,] feld, char spieler)
    {
        for (int i = 0; i < 3; i++)
        {
            if (feld[0, i] == spieler && feld[1, i] == spieler && feld[2, i] == spieler)
                return true;

            if (feld[i, 0] == spieler && feld[i, 1] == spieler && feld[i, 2] == spieler)
                return true;
        }

        if (feld[0, 0] == spieler && feld[1, 1] == spieler && feld[2, 2] == spieler)
            return true;

        if (feld[0, 2] == spieler && feld[1, 1] == spieler && feld[2, 0] == spieler)
            return true;

        return false;
    }
}