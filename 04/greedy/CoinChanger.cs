public class CoinChanger
{
    private static int[] coins = new int[] { 200, 100, 50, 20, 10, 5, 2, 1 };

    public static Dictionary<int, int> Change(double betrag)
    {
        int cent = (int)(betrag * 100);
        var change = new Dictionary<int, int>();

        foreach (var coin in coins)
        {
            int coin_count = cent / coin;
            cent %= coin;

            if (coin_count > 0)
                change.Add(coin, coin_count);
        }

        return change;
    }
}