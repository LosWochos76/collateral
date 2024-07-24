using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class RabbitMQSenderFacade
{
    private ConnectionFactory connectionFactory;
    private IConnection connection;
    private IModel channel;
    private string queueNameAndRoutingKey = "email";

    public RabbitMQSenderFacade()
    {
        connectionFactory = new ConnectionFactory() { HostName = "localhost" };
        connection = connectionFactory.CreateConnection();
        channel = connection.CreateModel();
        channel.QueueDeclare(queue: queueNameAndRoutingKey, durable: true, exclusive: false, autoDelete: false, arguments: null);
    }

    public void Send(SendEmailMessage message)
    {
        var body = JsonSerializer.SerializeToUtf8Bytes(message);
        channel.BasicPublish(exchange: string.Empty, routingKey: queueNameAndRoutingKey, basicProperties: null, body: body);
    }
}