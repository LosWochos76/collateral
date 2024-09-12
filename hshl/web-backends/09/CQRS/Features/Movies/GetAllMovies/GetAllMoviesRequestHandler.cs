using FluentResults;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Features.Movies.GetAllMovies;

public record GetAllMoviesRequest() : IRequest<List<Movie>>;

public class GetAllMoviesRequestHandler(AppDbContext dbContext) : IRequestHandler<GetAllMoviesRequest, List<Movie>>
{
    public async Task<List<Movie>> Handle(GetAllMoviesRequest request, CancellationToken cancellationToken)
    {
        var objects = await dbContext.Movies.ToListAsync(cancellationToken);
        return objects;
    }
}