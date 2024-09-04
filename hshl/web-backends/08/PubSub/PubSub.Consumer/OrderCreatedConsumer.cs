using MassTransit;
using PubSub.Shared;

namespace PubSub.Consumer;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        Console.WriteLine($"Received OrderCreated-message with guid {context.Message.id}");
        return Task.CompletedTask;
    }
}
