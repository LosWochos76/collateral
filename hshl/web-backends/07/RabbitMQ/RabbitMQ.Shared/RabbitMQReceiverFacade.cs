using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public delegate void MessageReceivedEventHandler(object sender, SendEmailMessage message);

public class RabbitMQReceiverFacade
{
    private ConnectionFactory connectionFactory;
    private IConnection connection;
    private IModel channel;
    private string queueNameAndRoutingKey = "email";
    private EventingBasicConsumer consumer;

    public event MessageReceivedEventHandler MessageReceived;

    public RabbitMQReceiverFacade()
    {
        connectionFactory = new ConnectionFactory() { HostName = "localhost" };
        connection = connectionFactory.CreateConnection();
        channel = connection.CreateModel();
        channel.QueueDeclare(queue: queueNameAndRoutingKey, durable: true, exclusive: false, autoDelete: false, arguments: null);
        
        consumer = new EventingBasicConsumer(channel);
        consumer.Received += MessageReceivedHandler;
        channel.BasicConsume(queue: queueNameAndRoutingKey, autoAck: true, consumer: consumer);
    }

    private void MessageReceivedHandler(object sender, BasicDeliverEventArgs e)
    {
        var bodyAsBytes = e.Body.ToArray();
        var message = JsonSerializer.Deserialize<SendEmailMessage>(bodyAsBytes);

        if (message is null)
            return;

        MessageReceived?.Invoke(this, message);
    }
}