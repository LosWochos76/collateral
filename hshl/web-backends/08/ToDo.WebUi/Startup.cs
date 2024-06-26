using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using ToDoManager.Common.Misc;
using ToDoManager.Persistence;
using ToDoManager.Persistence.EfCore;
using ToDoManager.WebUi.Misc;

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
        services.AddSingleton<PasswordHelper>();
        services.AddSingleton<DbConnectionFactory>();

        // Using Daper:
        //services.AddSingleton<IToDoRepository, ToDoDapperRepository>();
        //services.AddSingleton<IUserRepository, UserDapperRepository>();
        // End using Dapper */

        // Using EF-Core:
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IToDoRepository, ToDoEfCoreRepository>();
        services.AddScoped<IUserRepository, UserEfCoreRepository>();
        // end using EF-Core /

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

        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });

        var settings = app.ApplicationServices.GetRequiredService<IOptions<DatabaseSettings>>();
        Console.WriteLine(settings.Value.Host);
    }
}