using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Windows;

namespace SeminarManager;

class App : Application
{
    private static IHost host;

    [STAThread]
    public static void Main(string[] args)
    {
        var builder = new HostBuilder();

        builder.ConfigureHostConfiguration(configuration =>
        {
            configuration.SetBasePath(Directory.GetCurrentDirectory());
            configuration.AddJsonFile("launchsettings.json");
            configuration.AddCommandLine(args);
            configuration.AddEnvironmentVariables(prefix: "SEMINARMANAGER_");
        });

        builder.ConfigureAppConfiguration((context, configuration) =>
        {
            var env = context.HostingEnvironment;
            configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            configuration.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            configuration.AddEnvironmentVariables(prefix: "SEMINARMANAGER_");
            configuration.AddCommandLine(args);
        });

        builder.ConfigureLogging((context, loggingBuilder) =>
        {
            loggingBuilder.AddSimpleConsole(builder => {
                builder.SingleLine = true;
                builder.TimestampFormat = "[HH:mm:ss:F] ";
            });

            loggingBuilder.AddFile("SeminarManager.log");

            if (context.HostingEnvironment.IsDevelopment())
                loggingBuilder.SetMinimumLevel(LogLevel.Debug);
            else
                loggingBuilder.SetMinimumLevel(LogLevel.Warning);
        });

        builder.ConfigureServices((context, services) =>
        {
            services.Configure<SomeApiOptions>(context.Configuration.GetSection(SomeApiOptions.SectionName));
            services.AddSingleton<NavigationStore>();
            services.AddSingleton<DataRepository>();
            services.AddTransient<MainWindow>();
            services.AddTransient<MainViewModel>();
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
        host.Services.GetRequiredService<MainWindow>().Show();
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
    }
}