using MassTransit;
using PubSub.Shared;

namespace PubSub.Consumer;

public class OrderCreatedConsumer : IConsumer<OrderCreated>
{
    public Task Consume(ConsumeContext<OrderCreated> context)
    {
        Console.WriteLine($"Received OrderCreated-message with guid {context.Message.id}");
        return Task.CompletedTask;
    }
}
