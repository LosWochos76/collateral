using System.ComponentModel.DataAnnotations;

namespace ToDoUI.ViewModels;

public class PasswordForgottenViewModel
{
    [Required]
    [EmailAddress]
    public string EMail { get; set; }
}