using System;
using CQRS.Features.Movies.CreateMovie;
using CQRS.Features.Movies.CreateRandomMovies;
using CQRS.Features.Movies.DeleteMovie;
using CQRS.Features.Movies.GetAllMovies;
using CQRS.Features.Movies.GetSingleMovie;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Features.Movies;

public class MovieController(IMediator mediator) : Controller
{
    [HttpPost("/api/movie")]
    public async Task<IActionResult> CreateMovie(CreateMovieCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        else
            return BadRequest(result.Errors);
    }

    [HttpGet("/api/movie/createRandom")]
    public async Task<IActionResult> CreateRandomMovies(CancellationToken cancellationToken = default)
    {
        await mediator.Send(new CreateRandomMoviesCommand());
        return Ok();
    }

    [HttpDelete("/api/movie/{id:guid}")]
    public async Task<IActionResult> DeleteMovie([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new DeleteMovieCommand(id), cancellationToken);
        if (result.IsSuccess)
            return Ok();
        else
            return BadRequest(result.Errors);
    }

    [HttpGet("/api/movie")]
    public async Task<IActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new GetAllMoviesRequest(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("/api/movie/{id:guid}")]
    public async Task<IActionResult> HandleAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new GetSingleMovieCommand(id), cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        else
            return BadRequest(result.Errors);
    }

    [HttpPut("/api/movie/{id:Guid}")]
    public async Task<IActionResult> HandleAsync(UpdateMovieCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        else
            return BadRequest(result.Errors);
    }
}