using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PubSub.Shared;

namespace PubSub.Producer.Controllers;

public class HomeController(ILogger<HomeController> logger, IPublishEndpoint endpoint) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateOrder()
    {
        var orderCreated = new OrderCreatedEvent(Guid.NewGuid());
        endpoint.Publish(orderCreated);
        return Redirect("/");
    }
}
