using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoService.Models;

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
    [Authorize]
    public IActionResult GetAll([FromBody] ToDoFilter filter)
    {
        var id = User.FindFirst("id").Value;
        var guid = Guid.Parse(id);
        var currentUser = userRepository.GetSingle(guid);

        if (currentUser.IsAdmin)
            return Ok(toDoRepository.GetAll(filter));
        else
            return Ok(toDoRepository.GetAllForUser(currentUser, filter));
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
        var currentUser = GetCurrentUser();
        var old = toDoRepository.GetSingle(obj.ID);

        if (!old.Owner.ID.Equals(currentUser.ID) || !currentUser.IsAdmin)
            return BadRequest("Not allowed!");

        toDoRepository.Update(obj);
        return Ok();
    }

    [Route("/ToDo/")]
    [HttpPost]
    [Authorize]
    public IActionResult Insert([FromBody] ToDo obj)
    {
        obj.Owner = GetCurrentUser();
        obj = toDoRepository.Add(obj);
        return Ok(obj);
    }

    [Route("/ToDo/{id}")]
    [HttpDelete]
    [Authorize]
    public IActionResult Delete(Guid id)
    {
        var currentUser = GetCurrentUser();
        var obj = toDoRepository.GetSingle(id);

        if (obj is null)
            return NotFound();

        if (!obj.Owner.ID.Equals(currentUser.ID) || !currentUser.IsAdmin)
            return BadRequest("Not allowed!");

        toDoRepository.Delete(id);
        return Ok();
    }

    private User GetCurrentUser()
    { 
        var id = User.FindFirst("id").Value;
        var guid = Guid.Parse(id);
        var user = userRepository.GetSingle(guid);
        return user;
    }
}