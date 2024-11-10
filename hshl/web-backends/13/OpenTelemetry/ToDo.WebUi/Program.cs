using OpenTelemetry.Logs;

namespace ToDoManager.WebUi;

public class Program
{
    public static void Main(string[] args)
    {
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

        builder.ConfigureLogging((hostingContext, logging) => {
            logging.AddOpenTelemetry(options => {
                options.AddOtlpExporter(options => {
                    options.Endpoint = new Uri("http://localhost:18889");
                });
            });
            logging.AddConsole();
        });

        builder.ConfigureWebHostDefaults(webBuilder => {
            webBuilder.UseStartup<Startup>();
        });

        var host = builder.Build();
        host.Run();
    }
}