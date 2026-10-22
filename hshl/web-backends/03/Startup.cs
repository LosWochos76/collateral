using System.Text.Json.Serialization;
using Asp.Versioning;
using Scalar.AspNetCore;
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
        services.AddSingleton<IToDoRepository, ToDoMemoryRepository>();
        
        services.AddControllers().AddJsonOptions(options => {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddMvc();
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddApiVersioning(options => {
            options.DefaultApiVersion = new ApiVersion(2);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddMvc().AddApiExplorer(options => {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        // one OpenAPI-document per API-version (matches the group names "'v'V")
        services.AddOpenApi("v1");
        services.AddOpenApi("v2");
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseExceptionHandler();
        app.UseRouting();

        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();

            if (env.IsDevelopment())
            {
                endpoints.MapOpenApi(); // /openapi/v1.json and /openapi/v2.json
                endpoints.MapScalarApiReference(options =>
                {
                    options.AddDocument("v1", "ToDoService v1");
                    options.AddDocument("v2", "ToDoService v2");
                }); // /scalar
            }
        });
    }
}