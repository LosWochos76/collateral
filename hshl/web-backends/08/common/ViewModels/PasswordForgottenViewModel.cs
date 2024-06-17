using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels;

public class PasswordForgottenViewModel
{
    [Required]
    [EmailAddress]
    public string EMail { get; set; }
}