using BundesligaDatenLader;

public class Program
{
    public static void Main()
    {
        var connection_string = "Host=172.25.200.52;Username=hshl;Password=hshl;Database=bundesliga";
        var tl = new VereineLoader(2023, connection_string);
        tl.Import();

        var sl = new SpieleLoader(2023, connection_string);
        sl.Import();
    }
}