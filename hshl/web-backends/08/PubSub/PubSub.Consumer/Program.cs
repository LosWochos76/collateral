using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using PubSub.Consumer;

var services = new ServiceCollection();

services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
    });
});

var serviceProvider = services.BuildServiceProvider();
var bus = serviceProvider.GetRequiredService<IBusControl>();
await bus.StartAsync();

Console.WriteLine("Waiting for messages...");
Console.ReadLine();

await bus.StopAsync();