using System.Text;
using System.Text.Json.Serialization;
using Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ToDoService.Misc;

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
        services.Configure<JwtSettings>(config.GetSection("Jwt"));
        services.AddSingleton<JwtTokenHelper>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

        services.AddControllers().AddJsonOptions(options => {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddSingleton<IToDoRepository, ToDoMemoryRepository>();
        services.AddSingleton<IUserRepository, UserMemoryRepository>();

        if (UseKeycloak)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o => {
                    o.Authority = config["Authentication:Keycloak:Authority"];
                    o.Audience = config["Authentication:Keycloak:Audience"];
                    o.MapInboundClaims = false;
                    o.TokenValidationParameters.NameClaimType = "preferred_username";
                    o.TokenValidationParameters.RoleClaimType = "roles";
                    o.RequireHttpsMetadata = !env.IsDevelopment();
                });
        }
        else
        {
            var jwt = config.GetSection("Jwt").Get<JwtSettings>()!;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.MapInboundClaims = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwt.Issuer,
                        ValidAudience = jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                        NameClaimType = "email",
                        RoleClaimType = "role"
                    };
                });
        }

        services.AddAuthorization(options => {
            // all endpoints require an authenticated user, unless they are marked with [AllowAnonymous]
            options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            options.AddPolicy("AdminWithEmail", p => p.RequireRole("Admin").RequireClaim("email"));
            options.AddPolicy("AtLeast18", p => p.AddRequirements(new MinimumAgeRequirement(18)));
        });

        services.AddSingleton<IAuthorizationHandler, MinimumAgeAuthorizationHandler>();
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
