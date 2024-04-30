using System.Text.Json.Serialization;
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
        services.AddControllers().AddJsonOptions(options => {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        services.AddSingleton<IToDoRepository, ToDoMemoryRepository>();

        services.AddSwaggerGen(c => 
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoApi", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoApi");
        });

        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
    }
}