public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseMiddleware<CustomMiddleware>();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseEndpoints(endpoints => {
            endpoints.MapGet("/", async context => {
                await context.Response.WriteAsync("Hello, World!");
            });


            /*endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");*/
        });
    }
}