using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using PubSub.Consumer;

var services = new ServiceCollection();

Console.WriteLine("Bitte Queue-Namen für den Konsumer eingeben: ");
var queue = Console.ReadLine();

services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint(queue, e => {
            e.Consumer<OrderCreatedConsumer>();
        });
    });
});

var serviceProvider = services.BuildServiceProvider();
var bus = serviceProvider.GetRequiredService<IBusControl>();
await bus.StartAsync();

Console.WriteLine("Waiting for messages...");
Console.ReadLine();