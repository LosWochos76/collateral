using System.ComponentModel.DataAnnotations;

namespace ToDoManager.WebUi.ViewModels;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string EMail { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}