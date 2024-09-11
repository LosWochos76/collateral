
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VerticalSlicing.Features.Movies.CreateRandomMovies;

public class CreateRandomMoviesEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<IActionResult>
{
    private IMediator mediator;

    public CreateRandomMoviesEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("/api/movie/createRandom")]
    public async override Task<ActionResult<IActionResult>> HandleAsync(CancellationToken cancellationToken = default)
    {
        await mediator.Send(new CreateRandomMoviesCommand());
        return Ok();
    }
}