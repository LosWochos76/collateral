using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ToDoUI.Models;

public class ToDo : Entity
{
    [Required]
    [MinLength(10, ErrorMessage = "The title must have a length of minimum 10 characters!")]
    public string Title { get; set; }

    [Required]
    [Range(0,100,ErrorMessage = "Value must be between 0 and 100!")]
    public int Completion { get; set; }

    [ValidateNever]
    public string Description { get; set; }
}