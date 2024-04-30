using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoService.Models;

namespace ToDoService.Controllers;

public class TodoController : Controller
{
    private readonly ILogger<TodoController> logger;
    private readonly IToDoRepository toDoRepository;

    public TodoController(ILogger<TodoController> logger, IToDoRepository toDoRepository)
    {
        this.logger = logger;
        this.toDoRepository = toDoRepository;
    }

    [Route("/ToDo/")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(ToDoListResult))]
    public IActionResult GetAll([FromBody] ToDoFilter filter)
    {
        return Ok(toDoRepository.GetAll(filter));
    }

    [Route("/ToDo/{id}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetSingle([FromRoute] Guid id)
    {
        var obj = toDoRepository.GetSingle(id);
        if (obj is null)
            return NotFound();
        
        return Ok(obj);
    }

    [Route("/ToDo/{id}")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update([FromBody] ToDo obj)
    {
        var old = toDoRepository.GetSingle(obj.ID);
        if (old is null)
            return NotFound();

        return Ok(toDoRepository.Update(obj));
    }

    [Route("/ToDo/")]
    [HttpPost]
    public IActionResult Insert([FromBody] ToDo obj)
    {
        return Ok(toDoRepository.Add(obj));
    }

    [Route("/ToDo/{id}")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        var obj = toDoRepository.GetSingle(id);
        if (obj is null)
            return NotFound();

        toDoRepository.Delete(id);
        return Ok();
    }
}