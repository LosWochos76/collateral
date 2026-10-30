using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string EMail { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}