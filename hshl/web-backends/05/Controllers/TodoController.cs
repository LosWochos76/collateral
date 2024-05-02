using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ToDoService.Models;

namespace ToDoService.Controllers;

[ApiVersion(1)]
[ApiVersion(2)]
[ApiController]
[Route("/api/v{v:apiVersion}/ToDo/")]
public class TodoControllerV1 : Controller
{
    private readonly ILogger<TodoControllerV1> logger;
    private readonly IToDoRepository toDoRepository;

    public TodoControllerV1(ILogger<TodoControllerV1> logger, IToDoRepository toDoRepository)
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
    public IActionResult GetAllV2([FromBody] ToDoFilter filter)
    {
        return Ok(toDoRepository.GetAll(filter));
    }

    [Route("{id}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetSingle([FromRoute] Guid id)
    {
        var obj = toDoRepository.GetSingle(id);
        if (obj is null)
            return NotFound(new ProblemDetails() { Title  = $"The ToDo-object with the ID {id} was not found!" });
        
        return Ok(obj);
    }

    [Route("/{id}")]
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

    [HttpPost]
    public IActionResult Insert([FromBody] ToDo obj)
    {
        return Ok(toDoRepository.Add(obj));
    }

    [Route("/{id}")]
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