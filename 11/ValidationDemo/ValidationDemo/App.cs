using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace ValidationDemo;

public class App : Application
{
    [STAThread]
    public static void Main()
    {
        var app = new App();
        app.Run();
    }

    public App()
    {
        Services = ConfigureServices();
    }

    public IServiceProvider Services { get; }
    public new static App Current => (App)Application.Current;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var window = new MainWindow();
        MainWindow = window;
        window.Show();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddTransient<EmailDialogViewModel>();
        services.AddTransient<PersonDialogViewModel>();

        return services.BuildServiceProvider();
    }
}