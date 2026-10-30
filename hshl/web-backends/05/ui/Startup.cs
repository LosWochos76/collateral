using Common.Misc;
using Common.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using ToDoUI.Misc;

namespace ToDoUI;

public class Startup
{
    private IConfiguration config;
    private IWebHostEnvironment env;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        this.config = configuration;
        this.env = env;
    }

    private bool UseKeycloak => config["Authentication:Mode"] == "Keycloak";

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<MailSettings>(options => config.GetSection("MailSettings").Bind(options));
        services.Configure<GeneralSettings>(options => config.GetSection("GeneralSettings").Bind(options));

        services.AddSingleton<PasswordHelper>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddSingleton<IToDoRepository, ToDoMemoryRepository>();
        services.AddSingleton<IUserRepository, UserMemoryRepository>();
        services.AddSingleton<EmailQueue>();
        services.AddSingleton<EmailService>();
        services.AddHostedService<BackgroundEmailSender>();
    
        // validate the antiforgery-token of all POST-, PUT- and DELETE-requests
        services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
        services.AddProblemDetails();

        if (UseKeycloak)
            AddKeycloakAuthentication(services);
        else
            AddLocalAuthentication(services);
    }

    private void AddLocalAuthentication(IServiceCollection services)
    {
        var authentication = services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Authentication/Login";
                options.AccessDeniedPath = "/Authentication/AccessDenied";
            })
            .AddCookie("External");  // short-lived cookie for the identity of an external provider

        // external providers are only available, if their credentials are configured (dotnet user-secrets)
        if (!string.IsNullOrEmpty(config["Authentication:Google:ClientId"]))
            authentication.AddGoogle(o => {
                o.SignInScheme = "External";
                o.ClientId = config["Authentication:Google:ClientId"]!;
                o.ClientSecret = config["Authentication:Google:ClientSecret"]!;
            });

        if (!string.IsNullOrEmpty(config["Authentication:Facebook:AppId"]))
            authentication.AddFacebook(o => {
                o.SignInScheme = "External";
                o.AppId = config["Authentication:Facebook:AppId"]!;
                o.AppSecret = config["Authentication:Facebook:AppSecret"]!;
            });
    }

    private void AddKeycloakAuthentication(IServiceCollection services)
    {
        services.AddAuthentication(o => {
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme; })
            .AddCookie(o => o.AccessDeniedPath = "/Authentication/AccessDenied")
            .AddOpenIdConnect(o => {
                o.Authority = config["Authentication:Keycloak:Authority"];
                o.ClientId = config["Authentication:Keycloak:ClientId"];
                o.ClientSecret = config["Authentication:Keycloak:ClientSecret"];
                o.ResponseType = OpenIdConnectResponseType.Code;
                o.MapInboundClaims = false;
                o.TokenValidationParameters.NameClaimType = "preferred_username";
                o.TokenValidationParameters.RoleClaimType = "roles";
                o.RequireHttpsMetadata = !env.IsDevelopment();
            });
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseExceptionHandler();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=ToDo}/{action=List}/{id?}");
        });
    }
}
