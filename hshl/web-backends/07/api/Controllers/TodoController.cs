using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDoService.Controllers;

public class TodoController : Controller
{
    private readonly ILogger<TodoController> logger;
    private readonly IToDoRepository toDoRepository;
    private readonly IUserRepository userRepository;

    public TodoController(
        ILogger<TodoController> logger, 
        IToDoRepository toDoRepository, 
        IUserRepository userRepository)
    {
        this.logger = logger;
        this.toDoRepository = toDoRepository;
        this.userRepository = userRepository;
    }

    [Route("/ToDo/")]
    [HttpGet]
    [Authorize(Policy = "AtLeast18")]
    public IActionResult GetAll([FromBody] ToDoFilter filter)
    {
        return Ok(toDoRepository.GetAll(filter));
    }

    [Route("/ToDo/{id}")]
    [HttpGet]
    [Authorize]
    public IActionResult GetSingle([FromRoute] Guid id)
    {
        var obj = toDoRepository.GetSingle(id);
        if (obj is null)
            return NotFound();
        
        return Ok(obj);
    }

    [Route("/ToDo/{id}")]
    [HttpPut]
    [Authorize]
    public IActionResult Update([FromBody] ToDo obj)
    {
        toDoRepository.Update(obj);
        return Ok();
    }

    [Route("/ToDo/")]
    [HttpPost]
    [Authorize]
    public IActionResult Insert([FromBody] ToDo obj)
    {
        obj = toDoRepository.Add(obj);
        return Ok(obj);
    }

    [Route("/ToDo/{id}")]
    [HttpDelete]
    [Authorize]
    public IActionResult Delete(Guid id)
    {
        var obj = toDoRepository.GetSingle(id);
        if (obj is null)
            return NotFound();

        toDoRepository.Delete(id);
        return Ok();
    }
}