using System.ComponentModel.DataAnnotations;

namespace ToDoManager.Api.ViewModels;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string EMail { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
