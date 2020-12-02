using Culture;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeminarManager.EF;
using SeminarManager.Model;
using SeminarManager.SQL;

namespace SeminarManager.API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            var persistence_method = Helper.GetFromEnvironmentOrDefault("PERSISTENCE_METHOD", "memory");

            if (persistence_method.Equals("memory"))
                services.AddSingleton<IRepository, MemoryRepository>();
            
            if (persistence_method.Equals("sql"))
                services.AddSingleton<IRepository, SqlRepository>();

            if (persistence_method.Equals("ef"))
                services.AddSingleton<IRepository, EfRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCustomAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Person}/{action=Index}/{id?}"
                );
            });
        }
    }
}
