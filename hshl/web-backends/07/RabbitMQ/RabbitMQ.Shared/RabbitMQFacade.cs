using System;
using RabbitMQ.Client;

namespace RabbitMQ.Shared;

public abstract class RabbitMQFacade
{
    protected ConnectionFactory connectionFactory;
    protected IConnection connection;
    protected IModel model;

    public RabbitMQFacade()
    {
        connectionFactory = new ConnectionFactory() { HostName = "localhost" };
        connection = connectionFactory.CreateConnection();
        model  = connection.CreateModel();
        model.ExchangeDeclare("email_exchange", ExchangeType.Direct);
        model.QueueDeclare("email_queue", true, false, false, null);
        model.QueueBind("email_queue", "email_exchange", string.Empty);
    }
}
