using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ToDoService.Models;

namespace ToDoService.Controllers;

[ApiVersion(1)]
[ApiVersion(2)]
[ApiController]
[Route("/api/v{v:apiVersion}/ToDo/")]
public class TodoController : Controller
{
    private readonly ILogger<TodoController> logger;
    private readonly IToDoRepository toDoRepository;

    public TodoController(ILogger<TodoController> logger, IToDoRepository toDoRepository)
    {
        this.logger = logger;
        this.toDoRepository = toDoRepository;
    }

    [HttpGet]
    [MapToApiVersion(1)]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<ToDo>))]
    public IActionResult GetAll()
    {
        return Ok(toDoRepository.GetAll());
    }

    [HttpGet]
    [MapToApiVersion(2)]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(ToDoListResult))]
    public IActionResult GetAllV2([FromQuery] ToDoFilter filter)
    {
        return Ok(toDoRepository.GetAll(filter));
    }

    [Route("{id}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(ToDo))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type=typeof(ProblemDetails))]
    public IActionResult GetSingle([FromRoute] Guid id)
    {
        var obj = toDoRepository.GetSingle(id);
        if (obj is null)
            return NotFound(new ProblemDetails() { Title  = $"The ToDo-object with the ID {id} was not found!" });
        
        return Ok(obj);
    }

    [Route("{id}")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(ToDo))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update([FromRoute] Guid id, [FromBody] ToDo obj)
    {
        var old = toDoRepository.GetSingle(id);
        if (old is null)
            return NotFound();

        obj.ID = id;
        return Ok(toDoRepository.Update(obj));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type=typeof(ToDo))]
    public IActionResult Insert([FromBody] ToDo obj)
    {
        var created = toDoRepository.Add(obj);
        return CreatedAtAction(nameof(GetSingle), new { id = created.ID }, created);
    }

    [Route("{id}")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var obj = toDoRepository.GetSingle(id);
        if (obj is null)
            return NotFound();

        toDoRepository.Delete(id);
        return Ok();
    }
}