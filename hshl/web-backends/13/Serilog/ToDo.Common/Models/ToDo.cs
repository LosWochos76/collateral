using System.ComponentModel.DataAnnotations;

namespace ToDoManager.Common.Models;

public class ToDo : Entity, IValidatableObject
{
    [Required]
    [MinLength(10, ErrorMessage = "The title must have a length of minimum 10 characters!")]
    public string Title { get; set; }

    [Required]
    [Range(0,100,ErrorMessage = "Value must be between 0 and 100!")]
    public int Completion { get; set; }

    public string Description { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Description.Equals(Title))
            yield return new ValidationResult("Title and Description canot be equal", new List<string>() { "Description" });
    }
}