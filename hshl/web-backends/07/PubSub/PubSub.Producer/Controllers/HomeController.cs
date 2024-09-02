using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PubSub.Shared;

namespace PubSub.Producer.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;
    private readonly IBus bus;

    public HomeController(ILogger<HomeController> logger, IBus bus)
    {
        this.logger = logger;
        this.bus = bus;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateOrder()
    {
        var orderCreated = new OrderCreatedEvent(Guid.NewGuid());
        bus.Publish(orderCreated);

        return Redirect("/");
    }
}
