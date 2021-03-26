using System;
using System.Collections;
using System.Collections.Generic;

namespace OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            var spiel = new TicTacToe();
            spiel.Zufallsfeld();
            spiel.Ausgeben();
            Console.WriteLine(spiel.Gewonnen('X'));
        }
    }
}

