using System;
using Culture;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeminarManager.Model;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;
using SeminarManager.SQL;
using SeminarManager.EF;

namespace SeminarManager.MVC
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            var redis_connection_string = Helper.GetFromEnvironmentOrDefault("REDIS_CONNECTION_STRING", "");
            if (redis_connection_string == string.Empty) 
            {
                services.AddDistributedMemoryCache();
            }
            else 
            {
                var redis = ConnectionMultiplexer.Connect(redis_connection_string);
                services.AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys")
                    .SetApplicationName("seminarmanager");

                services.AddStackExchangeRedisCache(o =>
                {
                    o.Configuration = redis_connection_string;
                    o.InstanceName = "seminarmanager";
                });
            }

            services.AddSession();
            services.AddControllersWithViews();

            var persistence_method = Helper.GetFromEnvironmentOrDefault("PERSISTENCE_METHOD", "memory");

            if (persistence_method.Equals("memory"))
                services.AddSingleton<IRepository, MemoryRepository>();
            
            if (persistence_method.Equals("sql"))
                services.AddSingleton<IRepository, SqlRepository>();

            if (persistence_method.Equals("ef"))
                services.AddSingleton<IRepository, EfRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSession();
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
