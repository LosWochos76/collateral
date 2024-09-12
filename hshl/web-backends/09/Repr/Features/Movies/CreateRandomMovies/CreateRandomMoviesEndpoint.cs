
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Repr.Features.Movies.CreateRandomMovies;

public class CreateRandomMoviesEndpoint(AppDbContext dbContext) : Controller
{
    [HttpGet("/api/movie/createRandom")]
    public async Task<IActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        dbContext.Database.ExecuteSqlRaw("delete from movies");

        var faker = new Faker<Movie>()
            .RuleFor(m => m.Id, f => Guid.NewGuid())
            .RuleFor(m => m.Title, f => f.Lorem.Sentence(3, 5));

        foreach (var movie in faker.Generate(100))
            dbContext.Movies.Add(movie);
        
        await dbContext.SaveChangesAsync();
        return Ok();
    }
}