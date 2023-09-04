using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace ValidationDemo;

public partial class PersonDialogViewModel : ObservableValidator
{
    public PersonDialogViewModel(string name, int age, string email)
    {
        Name = name;
        Age = age;
        Email = email;
    }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(AllowEmptyStrings = false)]
    [MinLength(3)]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private string name;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [Range(19, 99)]
    private int age;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(AllowEmptyStrings = false)]
    [EmailAddress]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private string email;

    public bool HasNoErrors { get { return !HasErrors; } }

    [RelayCommand(CanExecute=nameof(HasNoErrors))]
    public void Ok(Window window)
    {
        if (window is not null)
        {
            window.DialogResult = true;
            window.Close();
        }
    }

    public void Cancel(Window window)
    {
        if (window is not null)
        {
            window.DialogResult = false;
            window.Close();
        }
    }

    public PersonDialogViewModel Clone()
    {
        return new PersonDialogViewModel(Name, Age, Email);
    }
}