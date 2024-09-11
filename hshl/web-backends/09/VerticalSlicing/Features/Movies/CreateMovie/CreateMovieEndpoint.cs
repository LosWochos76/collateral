
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VerticalSlicing.Features.Movies.CreateMovie;

public class CreateMovieEndpoint : EndpointBaseAsync
    .WithRequest<CreateMovieCommand>
    .WithActionResult<Movie>
{
    private IMediator mediator;

    public CreateMovieEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("/api/movie")]
    public async override Task<ActionResult<Movie>> HandleAsync(CreateMovieCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        else
            return BadRequest(result.Errors);
    }
}