using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MvcApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

     public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return Content("Hello, World!");
    }

    public IActionResult Redirect()
    {
        return Redirect("/");
    }

    public IActionResult Error()
    {
        return NotFound();
        //return StatusCode(StatusCodes.Status404NotFound);
    }

    public IActionResult ToDo()
    {
        var obj = new {
            ID = Guid.NewGuid(),
            Title = "Test",
            Completion = 0.5,
            Description = "A new ToDo-Item"
        };

        return Ok(obj);
    }

    public IActionResult About()
    {
        var items = new[] { "First", "Second", "Third" };
        return View(items); // delivers content of /Views/Home/About.cshtml
    }

    public async Task<IActionResult> Login()
    {
        var context = HttpContext;
        var form = await context.Request.ReadFormAsync();
        var username = form["username"];
        var password = form["password"];

        if (username == "admin" && password == "secret")
            return Redirect("/data");
        else
            return View();
    }

    public IActionResult FromRoute([FromRoute] int? id)
    {
        if (id is null)
            return Content($"No ID found!");
        else
            return Content($"ID {id} was passed!");
    }

    public IActionResult FromQuery([FromQuery] int? page)
    {
        if (page is null)
            return Content($"No page found!");
        else
            return Content($"Page is {page}!");
    }
}
