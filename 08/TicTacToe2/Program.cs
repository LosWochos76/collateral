using System;

class Program
{
    static ISpieler WaehleSpieler(char spielstein)
    {
        int code;

        do {
            Console.WriteLine("Waehle " + spielstein + "!");
            Console.WriteLine("1 == Computer");
            Console.WriteLine("2 == Mensch");
            code = Convert.ToInt32(Console.ReadLine());
        }
        while (code < 1 || code > 2);

        if (code == 1)
            return new ComputerSpieler(spielstein);
        else
            return new MenschlicherSpieler(spielstein);
    }

    static void Main(string[] args)
    {
        // Diese Variante des TicTacToe-Spiels nutz Polymporphie mit Hilfe der Schnittstelle ISpieler
        TicTacToe ttt = new TicTacToe();
        ttt.Spieler1 = WaehleSpieler('X');
        ttt.Spieler2 = WaehleSpieler('O');
        ttt.StarteSpiel();
    }
}