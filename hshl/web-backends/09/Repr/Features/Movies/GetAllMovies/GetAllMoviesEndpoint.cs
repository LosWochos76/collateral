using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Repr.Features.Movies.GetAllMovies;

public class GetAllMoviesEndpoint(AppDbContext dbContext) : Controller
{
    [HttpGet("/api/movie")]
    public async Task<IActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        var objects = await dbContext.Movies.ToListAsync(cancellationToken);
        return Ok(objects);
    }
}