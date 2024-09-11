
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentResults;

namespace VerticalSlicing.Features.Movies.DeleteMovie;

public class DeleteMovieEndpoint : EndpointBaseAsync
    .WithRequest<DeleteMovieCommand>
    .WithActionResult
{
    private IMediator mediator;

    public DeleteMovieEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpDelete("/api/movie/{id:guid}")]
    public async override Task<ActionResult> HandleAsync(DeleteMovieCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        if (result.IsSuccess)
            return Ok();
        else
            return BadRequest(result.Errors);
    }
}