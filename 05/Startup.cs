using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ToDoService.Misc;
using ToDoService.Models;

public class Startup
{
    private IConfiguration config;

    public Startup(IConfiguration configuration)
    {
        this.config = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options => {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        services.AddSingleton<IToDoRepository, ToDoMemoryRepository>();

        var r2 = new UserMemoryRepository(config);
        r2.Add(new User() {
            ID = Guid.NewGuid(),
            Firstname = "Alexander", 
            Lastname = "Stucknholz", 
            EMail = "alexander.stuckenholz@hshl.de", 
            PasswordHash = "kZpD09nuVLSGBI9m+wYCSjVJjojh1fMv+ZGiOqBMvOg=", 
            IsAdmin = true
        });
        services.AddSingleton<IUserRepository>(r2);

        services.AddSingleton<JwtTokenHelper>();

        var jwtIssuer = config.GetSection("Jwt:Issuer").Get<string>();
        var jwtKey = config.GetSection("Jwt:Key").Get<string>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };
        });
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