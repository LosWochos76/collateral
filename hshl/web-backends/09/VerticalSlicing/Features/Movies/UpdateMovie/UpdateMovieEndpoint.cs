
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VerticalSlicing.Features.Movies.UpdateMovie;

public class UpdateMovieEndpoint : EndpointBaseAsync
    .WithRequest<UpdateMovieCommand>
    .WithActionResult<Movie>
{
    private IMediator mediator;

    public UpdateMovieEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPut("/api/movie/{id:Guid}")]
    public async override Task<ActionResult<Movie>> HandleAsync(UpdateMovieCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        else
            return BadRequest(result.Errors);
    }
}