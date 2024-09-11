using FluentResults;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace VerticalSlicing.Features.Movies.GetSingleMovie;

public record GetSingleMovieCommand(Guid Id) : IRequest<Result<Movie>>;

public class GetSingleMovieCommandHandler(AppDbContext dbContext) : IRequestHandler<GetSingleMovieCommand, Result<Movie>>
{
    public async Task<Result<Movie>> Handle(GetSingleMovieCommand command, CancellationToken cancellationToken)
    {
        var movie = await dbContext.Movies.FindAsync(command.Id);
        if (movie == null)
            return Result.Fail("Movie not found!");

        return Result.Ok(movie);
    }
}