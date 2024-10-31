using Serilog;
using Serilog.Formatting.Compact;

namespace ToDoManager.WebUi;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        var builder = new HostBuilder();
        builder.UseContentRoot(Directory.GetCurrentDirectory());

        builder.UseSerilog((ctx,cfg)=>
        {
            cfg.Enrich.WithProperty("Application", ctx.HostingEnvironment.ApplicationName)
                .Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName)
                .WriteTo.Console(new RenderedCompactJsonFormatter());
        });

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