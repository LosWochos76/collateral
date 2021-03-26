using System;

namespace OOP
{
    public class TicTacToe
    {
        private char[,] spielfeld = new char[3, 3];

        public void Ausgeben()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("'" + spielfeld[i, j] + "' ");
                }

                Console.WriteLine();
            }
        }

        public bool Gewonnen(char spieler)
        {
            for (int i = 0; i < 3; i++)
            {
                if (spielfeld[0, i] == spieler && spielfeld[1, i] == spieler && spielfeld[2, i] == spieler)
                    return true;

                if (spielfeld[i, 0] == spieler && spielfeld[i, 1] == spieler && spielfeld[i, 2] == spieler)
                    return true;
            }

            if (spielfeld[0, 0] == spieler && spielfeld[1, 1] == spieler && spielfeld[2, 2] == spieler)
                return true;

            if (spielfeld[0, 2] == spieler && spielfeld[1, 1] == spieler && spielfeld[2, 0] == spieler)
                return true;

            return false;
        }

        public void Zufallsfeld()
        {
            Random rnd = new Random();

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    switch (rnd.Next(0, 3))
                    {
                        case 1: spielfeld[x, y] = 'X'; break;
                        case 2: spielfeld[x, y] = 'O'; break;
                        default: spielfeld[x, y] = ' '; break;
                    }
                }
            }
        }
    }
}
