using System;
using FluentValidation;
using LayeredArchitecture.Controllers;

namespace LayeredArchitecture.Misc;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(m => m.Title).NotEmpty();
    }
}

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(m => m.Title).NotEmpty();
    }
}