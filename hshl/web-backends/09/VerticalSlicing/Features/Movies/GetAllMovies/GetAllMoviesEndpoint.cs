
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VerticalSlicing.Features.Movies.GetAllMovies;

public class GetAllMoviesEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<List<Movie>>
{
    private IMediator mediator;

    public GetAllMoviesEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("/api/movie")]
    public override async Task<ActionResult<List<Movie>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new GetAllMoviesRequest(), cancellationToken);
        return Ok(result);
    }
}