using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RequestReply.Shared;

var services = new ServiceCollection();
services.AddLogging();
services.AddMassTransit(x => 
{
    x.UsingRabbitMq((context, cfg) => 
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        x.AddRequestClient<FindUserQuery>(new Uri("exchange:login-request"));
    });
});

services.AddSingleton<UserService>();
var serviceProvider = services.BuildServiceProvider();
var bus = serviceProvider.GetRequiredService<IBusControl>();
await bus.StartAsync(); 

var service = serviceProvider.GetRequiredService<UserService>();

Console.WriteLine("Press enter to send a request...");
while (true)
{
    Console.ReadLine();

    var result = await service.FindUserByEmailAsync("hans.wurst@gmail.com");
    if (result is not null)
        Console.WriteLine($"User found: {result.Email}");
}