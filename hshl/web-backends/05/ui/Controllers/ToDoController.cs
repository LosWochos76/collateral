using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult List([FromQuery] ToDoFilter filter)
    {
        var objects = toDoRepository.GetAll(filter);
        return View(objects);
    }

    [HttpGet("/ToDo/Edit/{id}")]
    [Authorize]
    public IActionResult Edit([FromRoute] Guid id)
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
    public IActionResult Save([FromForm] ToDo obj)
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