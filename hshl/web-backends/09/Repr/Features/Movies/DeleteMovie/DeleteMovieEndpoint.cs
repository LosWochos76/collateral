using Microsoft.AspNetCore.Mvc;
using FluentResults;

namespace Repr.Features.Movies.DeleteMovie;

public class DeleteMovieEndpoint(AppDbContext dbContext) : Controller
{
    [HttpDelete("/api/movie/{id:guid}")]
    public async Task<ActionResult> HandleAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var movie = await dbContext.Movies.FindAsync(id, cancellationToken);
        if (movie == null)
            return BadRequest("Movie not found!");

        dbContext.Movies.Remove(movie);
        await dbContext.SaveChangesAsync(cancellationToken);
        return Ok();
    }
}