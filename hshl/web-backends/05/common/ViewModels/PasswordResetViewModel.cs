using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels;

public class PasswordResetViewModel : IValidatableObject
{
    public string Token { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string PasswordRepeat { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!Password.Equals(PasswordRepeat))
            yield return new ValidationResult("Passwords are not equal!", new List<string> { "PasswordRepeat" });
    }
}