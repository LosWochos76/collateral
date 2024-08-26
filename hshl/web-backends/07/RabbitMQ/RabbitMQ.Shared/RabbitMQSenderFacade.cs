using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Shared;

public class RabbitMQSenderFacade : RabbitMQFacade
{
    public RabbitMQSenderFacade() : base()
    {
    }

    public void Send(SendEmailMessage message)
    {
        var body = JsonSerializer.SerializeToUtf8Bytes(message);
        channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: body);
    }
}