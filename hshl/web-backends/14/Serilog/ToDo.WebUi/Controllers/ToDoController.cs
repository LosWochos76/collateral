using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoManager.Common.Models;
using ToDoManager.Persistence;

namespace ToDoService.Controllers;

public class ToDoController : Controller
{
    private readonly ILogger<ToDoController> logger;
    private readonly IToDoRepository toDoRepository;

    public ToDoController(ILogger<ToDoController> logger, IToDoRepository toDoRepository)
    {
        this.logger = logger;
        this.toDoRepository = toDoRepository;
    }

    [Authorize]
    [HttpGet("/")]
    [HttpGet("/ToDo/Index/")]
    public IActionResult Index([FromQuery] ToDoFilter? filter)
    {
        if (filter.StartPage == -1)
            filter.StartPage = 0;

        if (string.IsNullOrEmpty(filter.OrderBy))
            filter.OrderBy = "Title";

        var objects = toDoRepository.GetAll(filter);
        return View(objects);
    }

    [HttpGet("/ToDo/Edit/{id}")]
    [Authorize]
    public async Task<IActionResult> Edit([FromRoute] Guid id)
    {
        var obj = toDoRepository.GetSingle(id);
        if (obj == null)
            return NotFound();

        return View(obj);
    }

    [HttpGet("/ToDo/New")]
    [Authorize]
    public IActionResult New()
    {
        return View("Edit", new ToDo());
    }

    [HttpPost("/ToDo/Save/")]
    [Authorize]
    public async Task<IActionResult> Save([FromForm] ToDo obj)
    {
        if (obj == null)
            return Redirect("/");

        if (!ModelState.IsValid)
            return View("Edit", obj);

        if (obj.ID == Guid.Empty)
            toDoRepository.Add(obj);
        else
            toDoRepository.Update(obj);
    
        return Redirect("/");
    }

    [HttpGet("/ToDo/Delete/{id}")]
    [Authorize]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var obj = toDoRepository.GetSingle(id);
        if (obj == null)
            return NotFound();
        
        toDoRepository.Delete(id);
        return Redirect("/");
    }
}