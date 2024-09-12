using FluentResults;
using FluentValidation;
using MediatR;

namespace CQRS.Features.Movies.CreateMovie;

public record CreateMovieCommand(string Title) : IRequest<Result<Guid>>;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(m => m.Title).NotEmpty();
    }
}

public class CreateMovieCommandHandler(AppDbContext dbContext, IValidator<CreateMovieCommand> validator) : IRequestHandler<CreateMovieCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateMovieCommand command, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
            return Result.Fail(validationResult.ToString());

        var movie = new Movie()
        {
            Id = Guid.NewGuid(),
            Title = command.Title
        };
        
        dbContext.Movies.Add(movie);
        await dbContext.SaveChangesAsync();
        return Result.Ok(movie.Id);
    }
}