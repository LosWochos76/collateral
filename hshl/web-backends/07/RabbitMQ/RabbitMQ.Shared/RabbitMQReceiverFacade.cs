using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Shared;

public delegate void MessageReceivedEventHandler(object sender, SendEmailMessage message);

public class RabbitMQReceiverFacade : RabbitMQFacade
{
    private EventingBasicConsumer consumer;
    public event MessageReceivedEventHandler MessageReceived;

    public RabbitMQReceiverFacade() : base()
    {
        consumer = new EventingBasicConsumer(channel);
        consumer.Received += MessageReceivedHandler;
        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
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