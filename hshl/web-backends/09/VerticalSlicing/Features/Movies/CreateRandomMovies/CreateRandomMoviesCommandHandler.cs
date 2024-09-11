using MediatR;
using Microsoft.EntityFrameworkCore;
using Bogus;
using FluentResults;

namespace VerticalSlicing.Features.Movies.CreateRandomMovies;

public record CreateRandomMoviesCommand() : IRequest;

public class CreateRandomMoviesCommandHandler(AppDbContext dbContext) : IRequestHandler<CreateRandomMoviesCommand>
{
    public async Task Handle(CreateRandomMoviesCommand command, CancellationToken cancellationToken)
    {
        dbContext.Database.ExecuteSqlRaw("delete from movies");

        var faker = new Faker<Movie>()
            .RuleFor(m => m.Id, f => Guid.NewGuid())
            .RuleFor(m => m.Title, f => f.Lorem.Sentence(3, 5));

        foreach (var movie in faker.Generate(100))
            dbContext.Movies.Add(movie);
        
        await dbContext.SaveChangesAsync();
    }
}