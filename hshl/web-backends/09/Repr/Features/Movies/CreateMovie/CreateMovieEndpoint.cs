using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Repr.Features.Movies.CreateMovie;

public record CreateMovieCommand(string Title);

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(m => m.Title).NotEmpty();
    }
}

public class CreateMovieEndpoint(IValidator<CreateMovieCommand> validator, AppDbContext dbContext) : Controller
{
    [HttpPost("/api/movie")]
    public async Task<IActionResult> HandleAsync([FromBody] CreateMovieCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.ToString());

        var movie = new Movie()
        {
            Id = Guid.NewGuid(),
            Title = command.Title
        };
        
        dbContext.Movies.Add(movie);
        await dbContext.SaveChangesAsync();
        return Ok(movie.Id);
    }
}

