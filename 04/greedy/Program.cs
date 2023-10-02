var change = CoinChanger.Change(4.23);
foreach (var coin in change)
    Console.WriteLine("{0} Mal {1} Cent.", coin.Value, coin.Key);
