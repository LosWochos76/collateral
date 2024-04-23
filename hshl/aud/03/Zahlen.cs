public class Zahlen
{
    public static bool IstPrimzahl(long n)
    {
        if (n < 2)
            return false;

        if (n == 2)
            return true;

        for (long i = 2; i <= n / 2; i++)
        {
            if (n % i == 0)
                return false;
        }

        return true;
    }
}