using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Grafana.Loki;

namespace ToDoManager.WebUi;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.GrafanaLoki("http://localhost:3100")
            .CreateLogger();

        var builder = new HostBuilder();
        builder.UseContentRoot(Directory.GetCurrentDirectory());

        builder.ConfigureAppConfiguration((hostingContext, config) => {
            var env = hostingContext.HostingEnvironment;
            config
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional:true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);
        });

        builder.ConfigureWebHostDefaults(webBuilder => {
            webBuilder.UseStartup<Startup>();
        });

        var host = builder.Build();
        host.Run();
    }
}