using System.Text.Json.Serialization;
using Asp.Versioning;
using Microsoft.OpenApi.Models;
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
        }).AddApiExplorer(options => {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoService v1", Version = "v1" });
            c.SwaggerDoc("v2", new OpenApiInfo { Title = "ToDoService v2", Version = "v2" });
        });
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseExceptionHandler();
        app.UseRouting();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoService v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "ToDoService v2");
            });
        }

        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
    }
}