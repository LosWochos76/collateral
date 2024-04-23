public class TicTacToe
{
    private static char[,] spielfeld = new char[3,3];

    public static void Start()
    {
        LoescheDasSpielfeld();

        while (!HatEinSpielerGewonnen())
        {
            GibSpielbrettAus();
            LasseDenMenschSpielen();

            if (HatMenschGewonnen())
                break;
            
            LasseDenComputerSpielen();
        }
    }

    public static void GibSpielbrettAus()
    {
        Console.WriteLine("+-+-+-+");
        for (int x=0; x<3; x++)
        {
            for (int y=0; y<3; y++)
            {
                Console.Write("|{0}", spielfeld[x, y]);
            }

            Console.WriteLine("|");
            Console.WriteLine("+-+-+-+");
        }
    }

    public static void LoescheDasSpielfeld()
    {
        for (int x=0; x<3; x++)
        {
            for (int y=0; y<3; y++)
            {
                spielfeld[x, y] = ' ';
            }
        }
    }

    public static bool HatEinSpielerGewonnen()
    {
        return false;
    }

    public static void LasseDenMenschSpielen()
    {
    }

    public static bool HatMenschGewonnen()
    {
        return false;
    }

    public static void LasseDenComputerSpielen()
    {
    }
}