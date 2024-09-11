using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VerticalSlicing.Features.Movies.GetSingleMovie;

public class GetSingleMovieEndpoint : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<Movie>
{
    private IMediator mediator;

    public GetSingleMovieEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("/api/movie/{id:guid}")]
    public async override Task<ActionResult<Movie>> HandleAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new GetSingleMovieCommand(id), cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        else
            return BadRequest(result.Errors);
    }
}