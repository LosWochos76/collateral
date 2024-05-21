using ToDoUI.Models;

namespace ToDoUI;

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
    
        services.AddMvc();
        services.AddProblemDetails();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseExceptionHandler();
        app.UseRouting();

        app.UseEndpoints(endpoints => {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=ToDo}/{action=Index}/{id?}");
        });
    }
}