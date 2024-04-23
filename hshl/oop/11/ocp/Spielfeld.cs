using System;

class Spielfeld
{
    private char[,] feld;

    public Spielfeld()
    {
        Initialisieren();
    }

    public void Initialisieren()
    {
        feld = new char[3, 3];
        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
                Setzen(x, y, ' ');
    }

    public void Setzen(int x, int y, char spieler)
    {
        feld[y, x] = spieler;
    }

    public void ZufaelligBelegen()
    {
        Random rnd = new Random();

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                switch (rnd.Next(0, 3))
                {
                    case 1: Setzen(x, y, 'X'); break;
                    case 2: Setzen(x, y, 'O'); break;
                    default: Setzen(x, y, ' '); break;
                }
            }
        }
    }

    public void Ausgeben()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write("'" + feld[i, j] + "' ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    public bool HatGewonnen(char spieler)
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

    public bool HatGewonnen()
    {
        return HatGewonnen('X') || HatGewonnen('O');
    }

    public bool IstBelegt(int x, int y)
    {
        return feld[y, x] != ' ';
    }

    public bool IstEinFeldFrei() 
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (feld[i, j] == ' ')
                    return true;

        return false;
    }
}