using BundesligaDatenLader;

public class Program
{
    public static void Main()
    {
        var connection_string = "Host=172.25.200.52;Username=hshl;Password=hshl";
        var dbfacade = new DatabaseFacade(2023, connection_string);
        dbfacade.ImportVereine();
    }
}