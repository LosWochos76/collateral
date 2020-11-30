using System;
using Culture;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeminarManager.Model;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;
using System.IO;
using SeminarManager.SQL;

namespace SeminarManager.MVC
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var redis_connection_string = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING");
            if (redis_connection_string != null && redis_connection_string != string.Empty) 
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
            else 
            {
                services.AddDistributedMemoryCache();
            }

            services.AddSession();

            services.AddControllersWithViews();
            services.AddSingleton<IRepository, SqlRepository>();
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
