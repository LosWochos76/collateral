using Microsoft.AspNetCore.Authentication.Cookies;
using ToDoManager.Common.Misc;
using ToDoManager.Persistence;
using ToDoManager.Persistence.EfCore;
using ToDoManager.WebUi.Misc;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Grafana.Loki;

namespace ToDoManager.WebUi;

public class Startup
{
    private IConfiguration config;

    public Startup(IConfiguration configuration)
    {
        this.config = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<DatabaseSettings>(options => config.GetSection("DatabaseSettings").Bind(options));
        services.Configure<MailSettings>(options => config.GetSection("MailSettings").Bind(options));
        services.Configure<GeneralSettings>(options => config.GetSection("GeneralSettings").Bind(options));

        services.AddSerilog(options =>
        {
            options.Enrich.WithProperty("Application", "ProductAPI")
                .Enrich.WithProperty("Environment", "Dev")
                .WriteTo.Console(new RenderedCompactJsonFormatter())
                .WriteTo.GrafanaLoki("http://localhost:3100");
        });
        
        services.AddSingleton<PasswordHelper>();
        services.AddSingleton<DbConnectionFactory>();
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IToDoRepository, ToDoEfCoreRepository>();
        services.AddScoped<IUserRepository, UserEfCoreRepository>();

        services.AddSingleton<EmailQueue>();
        services.AddSingleton<EmailService>();
        services.AddHostedService<BackgroundEmailSender>();
    
        services.AddMvc();
        services.AddProblemDetails();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.LoginPath = "/Authentication/Login"; 
            options.AccessDeniedPath = "/Authentication/Login";
        });
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseExceptionHandler();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSerilogRequestLogging();

        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
    }
}