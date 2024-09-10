using System;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bogus;
using MediatR;

namespace VerticalSlicing.Features.Movies;

[ApiController]
[Route("/api/[controller]")]
public class MovieController(IMediator mb ediator) : Controller
{
    [HttpGet]
    [Route("fillWithSomeData")]
    public async Task<IActionResult> FillWithSomeData()
    {
        mediator.Send(new CreateRandomMoviesCommand());
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSingle(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetSingleMovieCommand(id), cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        else
            return BadRequest(result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllMoviesRequest(), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateMovieCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        else
            return BadRequest(result.Errors);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMovieCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("ID missmatch");

        var result = await mediator.Send(command, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        else
            return BadRequest(result.Errors);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, [FromBody] DeleteMovieCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("ID missmatch");

        var result = await mediator.Send(command, cancellationToken);
        if (result.IsSuccess)
            return Ok();
        else
            return BadRequest(result.Errors);
    }
}

