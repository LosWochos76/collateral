using System.Text;
using System.Text.Json.Serialization;
using Common.Misc;
using Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using ToDoService.Misc;

public class Startup
{
    private IConfiguration config;

    public Startup(IConfiguration configuration)
    {
        this.config = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<GeneralSettings>(options => config.GetSection("GeneralSettings").Bind(options));
        services.AddSingleton<JwtTokenHelper>();
        services.AddSingleton<PasswordHelper>();
        
        services.AddControllers().AddJsonOptions(options => {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        services.AddSingleton<IToDoRepository, ToDoMemoryRepository>();
        services.AddSingleton<IUserRepository, UserMemoryRepository>();

        var settings = config.GetSection("GeneralSettings").Get<GeneralSettings>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "ToDoApi",
                    ValidAudience = "ToDoApi",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.EncryptionKey))
                };
            });
        
        services.AddAuthorization(options => {
            options.AddPolicy("AtLeast18", policyBuilder => policyBuilder.AddRequirements(new MinimumAgeRequirement(42)));
        });

        services.AddSingleton<IAuthorizationHandler, MimimumAgeAuthorizationHandler>();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
    }
}