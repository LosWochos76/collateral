var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = "localhost:6379";
});
builder.Services.AddOutputCache(options => {
    options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(60);
});

var app = builder.Build();
app.UseRouting();
app.UseOutputCache();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}").CacheOutput();

app.Run();
