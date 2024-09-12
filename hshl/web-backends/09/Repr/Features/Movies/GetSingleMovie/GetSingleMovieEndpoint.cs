using Microsoft.AspNetCore.Mvc;

namespace Repr.Features.Movies.GetSingleMovie;

public class GetSingleMovieEndpoint(AppDbContext dbContext) : Controller
{
    [HttpGet("/api/movie/{id:guid}")]
    public async Task<IActionResult> HandleAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var movie = await dbContext.Movies.FindAsync(id, cancellationToken);
        if (movie == null)
            return NotFound();

        return Ok(movie);
    }
}