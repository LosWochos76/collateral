using System.Collections.Immutable;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GraphSearch.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphSearch;

public partial class App : Application
{
    public IHost Host { get; private set; }
    
    public override void Initialize()
    {
        var hostBuilder = new HostBuilder();

        hostBuilder.ConfigureServices((context, services) =>
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>();
        });
        
        Host = hostBuilder.Build();
        Host.Start();
        
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Host.Services.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}