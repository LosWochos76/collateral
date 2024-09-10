using System;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bogus;

namespace LayeredArchitecture.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class MovieController(IServiceProvider services) : Controller
{
    [HttpGet]
    [Route("fillWithSomeData")]
    public async Task<IActionResult> FillWithSomeData()
    {
        var dbContext = services.GetService<AppDbContext>();
        dbContext.Database.ExecuteSqlRaw("delete from movies");

        var faker = new Faker<Movie>()
            .RuleFor(m => m.Id, f => Guid.NewGuid())
            .RuleFor(m => m.Title, f => f.Lorem.Sentence(3, 5));

        foreach (var movie in faker.Generate(100))
            dbContext.Movies.Add(movie);
        
        await dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var dbContext = services.GetService<AppDbContext>();
        var objects = await dbContext.Movies.ToListAsync();
        return Ok(objects);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateMovieCommand command)
    {
        var validator = services.GetService<IValidator<CreateMovieCommand>>();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.ToString());

        var movie = new Movie()
        {
            Id = Guid.NewGuid(),
            Title = command.Title
        };
        
        var dbContext = services.GetService<AppDbContext>();
        dbContext.Movies.Add(movie);
        await dbContext.SaveChangesAsync();
        return Ok(movie.Id);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateMovieCommand command)
    {
        var validator = services.GetService<IValidator<UpdateMovieCommand>>();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.ToString());

        var dbContext = services.GetService<AppDbContext>();
        var movie = await dbContext.Movies.FindAsync(id);
        if (movie == null)
            return BadRequest("Movie not found!");

        movie.Title = command.Title;
        await dbContext.SaveChangesAsync();
        return Ok(movie);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteMovieCommand command)
    {
        var dbContext = services.GetService<AppDbContext>();
        var movie = await dbContext.Movies.FindAsync(command.Guid);
        if (movie == null)
            return BadRequest("Movie not found!");

        dbContext.Movies.Remove(movie);
        await dbContext.SaveChangesAsync();
        return Ok();
    }
}

public record CreateMovieCommand(string Title);
public record DeleteMovieCommand(Guid Guid);
public record UpdateMovieCommand(string Title);
