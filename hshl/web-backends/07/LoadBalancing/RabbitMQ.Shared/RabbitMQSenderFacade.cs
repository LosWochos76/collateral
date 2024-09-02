using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Shared;

public class RabbitMQSenderFacade : RabbitMQFacade
{
    public RabbitMQSenderFacade() : base() { }

    public void Send(SendEmailCommand message)
    {
        var body = JsonSerializer.SerializeToUtf8Bytes(message);
        model.BasicPublish("email_exchange", string.Empty, body: body);
    }
}