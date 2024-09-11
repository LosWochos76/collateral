using FluentResults;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace VerticalSlicing.Features.Movies;

public record UpdateMovieCommand(Guid Id, string Title) : IRequest<Result<Movie>>;

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(m => m.Title).NotEmpty();
    }
}

public class UpdateMovieCommandHandler(AppDbContext dbContext, IValidator<UpdateMovieCommand> validator) : IRequestHandler<UpdateMovieCommand, Result<Movie>>
{
    public async Task<Result<Movie>> Handle(UpdateMovieCommand command, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
            return Result.Fail(validationResult.ToString());

        var movie = await dbContext.Movies.FindAsync(command.Id);
        if (movie == null)
            return Result.Fail("Movie not found!");

        movie.Title = command.Title;
        await dbContext.SaveChangesAsync();
        return Result.Ok(movie);
    }
}