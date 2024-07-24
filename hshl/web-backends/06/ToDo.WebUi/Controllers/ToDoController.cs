using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoManager.Common.Models;
using ToDoManager.CQRS;
using ToDoManager.Persistence;

namespace ToDoService.Controllers;

public class ToDoController : Controller
{
    private readonly ILogger<ToDoController> logger;
    private readonly IToDoRepository toDoRepository;
    private readonly ISender sender;

    public ToDoController(ILogger<ToDoController> logger, IToDoRepository toDoRepository, ISender sender)
    {
        this.logger = logger;
        this.toDoRepository = toDoRepository;
        this.sender = sender;
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
        var query = new GetSingleToDoByIdQuery(id);
        var obj = await sender.Send(query);
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

        var command = new SaveSingleToDoCommand(obj);
        await sender.Send(command);
    
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