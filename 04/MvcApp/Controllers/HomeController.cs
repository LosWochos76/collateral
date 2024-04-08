using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MvcApp.Controllers;

public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Route("/")]
    public IActionResult Index()
    {
        return new ContentResult() 
        { 
            Content = "Hello, World!", ContentType = "text/html" 
        };

        return Content("Hello, World!");
    }
}
