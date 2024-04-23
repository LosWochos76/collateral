using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace RandomDog;

public class App : Application
{
    private static IHost host;

    [STAThread]
    public static void Main()
    {
        var builder = new HostBuilder();
        builder.ConfigureServices((context, services) =>
        {
            services.AddHttpClient<DogService>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>();
        });

        host = builder.Build();

        var app = new App();
        app.Run();
    }

    public App()
    {
        host.Services.GetRequiredService<MainWindow>().Show();
    }
}
