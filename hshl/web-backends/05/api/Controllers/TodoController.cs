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
    public IActionResult GetAll([FromBody] ToDoFilter? filter)
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
    public IActionResult Update([FromRoute] Guid id, [FromBody] ToDo newObj)
    {
        var obj = toDoRepository.GetSingle(id);
        if (obj is null)
            return NotFound();

        newObj.ID = id;
        toDoRepository.Update(newObj);
        return Ok(newObj);
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