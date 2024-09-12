
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Repr.Features.Movies.UpdateMovie;

public record UpdateMovieCommand(Guid Id, string Title);

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(m => m.Title).NotEmpty();
    }
}

public class UpdateMovieEndpoint(IValidator<UpdateMovieCommand> validator, AppDbContext dbContext) : Controller
{
    [HttpPut("/api/movie/{id:Guid}")]
    public async Task<IActionResult> HandleAsync([FromRoute] Guid id, [FromBody] UpdateMovieCommand command, CancellationToken cancellationToken = default)
    {
        if (id != command.Id)
            return BadRequest("Id missmatch");

        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.ToString());

        var movie = await dbContext.Movies.FindAsync(command.Id);
        if (movie == null)
            return NotFound();

        movie.Title = command.Title;
        await dbContext.SaveChangesAsync();
        return Ok(movie);
    }
}