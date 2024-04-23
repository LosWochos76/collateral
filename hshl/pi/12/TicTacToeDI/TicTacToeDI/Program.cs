using Microsoft.Extensions.DependencyInjection;

public class Program
{
    private static IServiceProvider serviceprovider;

    public static void Main(string[] args)
    {
        ConfigureServices();

        var spieldfeld1 = serviceprovider.GetRequiredService<Spielfeld>();
        var spielfeld2 = serviceprovider.GetRequiredService<Spielfeld>();
        var s1 = ActivatorUtilities.CreateInstance<MenschlicherSpieler>(serviceprovider, 'X');
        var s2 = ActivatorUtilities.CreateInstance<ComputerSpieler>(serviceprovider, 'O');
        var ttt = ActivatorUtilities.CreateInstance<TicTacToe>(serviceprovider, s1, s2);

        ttt.StarteSpiel();
    }

    public static void ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddTransient<Spielfeld>();
        services.AddSingleton<TicTacToe>();
        services.AddTransient<Spieler, MenschlicherSpieler>();
        services.AddTransient<Spieler, ComputerSpieler>();
        serviceprovider = services.BuildServiceProvider();

       
    }
}