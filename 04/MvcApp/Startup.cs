public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddMvc();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        /*if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpointBuilder => 
        {
            endpointBuilder.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
        });*/

        app.Use(async (HttpContext context, RequestDelegate next) => {
            // do something before the next element in the pipeline
            await context.Response.WriteAsync("Hello world!");
            // do something after the next element in the pipeline
        });
    }
}