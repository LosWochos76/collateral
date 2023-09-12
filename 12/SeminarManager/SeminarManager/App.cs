using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SeminarManager;

class App : Application
{
    private static IHost host;

    [STAThread]
    public static void Main(string[] args)
    {
        var builder = new HostBuilder();

        builder.ConfigureAppConfiguration(configuration =>
        {
            configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            configuration.AddCommandLine(args);
            configuration.AddEnvironmentVariables("WPF1APP");
        });

        /*builder.ConfigureLogging((context, logging) =>
        {
            logging.AddConfiguration(context.Configuration);
            logging.AddSimpleConsole(configure => configure.SingleLine = true);
            logging.AddFile("Wpf1App.log");
        });*/

        builder.ConfigureServices((context, services) =>
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<MainViewModel>();
            services.AddSingleton<NavigationStore>();
            services.AddSingleton<DataRepository>();
            services.AddTransient<PersonListViewModel>();
            services.AddTransient<SeminarListViewModel>();
        });

        host = builder.Build();
        host.Start();

        var app = new App();
        app.Run();
    }

    public App()
    {
        var navigation = host.Services.GetService<NavigationStore>();
        navigation.CurrentViewModel = host.Services.GetService<PersonListViewModel>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var main = host.Services.GetRequiredService<MainWindow>();
        main.Show();

        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
    }
}