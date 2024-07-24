using Microsoft.AspNetCore.Mvc;

namespace RabbitMQ.Sender.Controllers;

public class HomeController(ILogger<HomeController> logger, RabbitMQSenderFacade sender) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Send()
    {
        sender.Send(new SendEmailMessage() {
            To = "alexander.stuckenholz@hshl.de",
            Subject = Guid.NewGuid().ToString(),
            Body = "This is a test-message"
        });

        return Redirect("/");
    }
}