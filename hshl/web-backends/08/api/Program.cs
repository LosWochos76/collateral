using System.Net;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = new HostBuilder();
        builder.UseContentRoot(Directory.GetCurrentDirectory());

        builder.ConfigureLogging(logging => {
            logging.AddConsole();
        });

        builder.ConfigureAppConfiguration((hostingContext, config) => {
            var env = hostingContext.HostingEnvironment;
            config.AddJsonFile("appsettings.json", optional: false);
            config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional:true);
            config.AddEnvironmentVariables();
            config.AddCommandLine(args);
        });

        builder.ConfigureWebHostDefaults(webBuilder => {
            webBuilder.UseStartup<Startup>();
        });

        var host = builder.Build();
        host.Run();
    }
}