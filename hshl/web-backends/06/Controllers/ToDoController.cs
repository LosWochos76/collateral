using Microsoft.AspNetCore.Mvc;
using ToDoUI.Models;

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

    public IActionResult Index()
    {
        var objects = toDoRepository.GetAll(null);
        return View(objects);
    }

    [HttpGet("/ToDo/Edit/{id}")]
    public IActionResult Edit([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
            return View(new ToDo());

        var obj = toDoRepository.GetSingle(id);
        if (obj == null)
            return NotFound();

        return View(obj);
    }

    [HttpPost("/ToDo/Edit")]
    public IActionResult Save([FromForm] ToDo obj)
    {
        if (obj == null)
            return Redirect("/");

        if (obj.ID == Guid.Empty)
            toDoRepository.Add(obj);
        else
            toDoRepository.Update(obj);
    
        return Redirect("/");
    }

    public IActionResult Delete([FromRoute] Guid id)
    {
        var obj = toDoRepository.GetSingle(id);
        if (obj == null)
            return NotFound();
        
        toDoRepository.Delete(id);
        return Redirect("/");
    }
}