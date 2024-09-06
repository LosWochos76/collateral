using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidation.Controllers;

public class HomeController(ILogger<HomeController> logger, IValidator<SubmitData> validator) : Controller
{
    public IActionResult Index()
    {
        return View(new SubmitData());
    }

    [HttpPost]
    public async Task<IActionResult> Submit([FromForm] SubmitData data)
    {
        if (data is null)
            return Redirect("/");
        
        var result = await validator.ValidateAsync(data);
        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            return View("Index", data);
        }
        
        return View();
    }
}
