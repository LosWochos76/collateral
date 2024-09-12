using FluentResults;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Features.Movies.DeleteMovie;

public record DeleteMovieCommand(Guid Id) : IRequest<Result>;

public class DeleteMovieCommandHandler(AppDbContext dbContext) : IRequestHandler<DeleteMovieCommand, Result>
{
    public async Task<Result> Handle(DeleteMovieCommand command, CancellationToken cancellationToken)
    {
        var movie = await dbContext.Movies.FindAsync(command.Id);
        if (movie == null)
            return Result.Fail("Movie not found!");

        dbContext.Movies.Remove(movie);
        await dbContext.SaveChangesAsync();
        return Result.Ok();
    }
}