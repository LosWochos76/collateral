using System;
using RabbitMQ.Client;

namespace RabbitMQ.Shared;

public abstract class RabbitMQFacade
{
    protected ConnectionFactory connectionFactory;
    protected IConnection connection;
    protected IModel channel;
    protected string queueName = "email";

    public RabbitMQFacade()
    {
        connectionFactory = new ConnectionFactory() { HostName = "localhost" };
        connection = connectionFactory.CreateConnection();
        channel = connection.CreateModel();
        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
    }
}
