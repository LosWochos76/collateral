public class TicTacToe
{
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
    }

    public static void LoescheDasSpielfeld()
    {
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