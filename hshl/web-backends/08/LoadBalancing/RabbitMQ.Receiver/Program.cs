var receiver = new RabbitMQReceiverFacade();

receiver.MessageReceived += (sender, e) => {
    Console.WriteLine($"Message received. To='{e.To}', Subject='{e.Subject}', Body='{e.Body}'");
};

Console.ReadLine();