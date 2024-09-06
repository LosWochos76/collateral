using System.Data;
using FluentValidation;

public class SubmitDataValidator : AbstractValidator<SubmitData> 
{
    public SubmitDataValidator()
    {
        RuleFor(data => data.Email)
            .NotEmpty().WithMessage("Your E-Mail-address cannot be empty")
            .EmailAddress().WithMessage("Not a valid E-mail-address!");
        
        RuleFor(data => data.Password)
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
    }
}