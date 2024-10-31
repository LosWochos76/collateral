using System.ComponentModel.DataAnnotations;

namespace ToDoManager.WebUi.ViewModels;

public class PasswordForgottenViewModel
{
    [Required]
    [EmailAddress]
    public string EMail { get; set; }
}