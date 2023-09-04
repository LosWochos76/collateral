using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;

namespace SeminarManager;

public partial class PersonEditViewModel : ObservableValidator
{
    private Person model;

    [ObservableProperty]
    [Required(AllowEmptyStrings = false)]
    [MinLength(3)]
    private string vorname = string.Empty;

    partial void OnVornameChanged(string? oldValue, string newValue)
    {
        ValidateProperty(newValue, nameof(Vorname));
    }

    [ObservableProperty]
    [Required(AllowEmptyStrings = false)]
    [MinLength(3)]
    private string nachname = string.Empty;

    partial void OnNachnameChanged(string? oldValue, string newValue)
    {
        ValidateProperty(newValue, nameof(Nachname));
    }

    [ObservableProperty]
    [Required]
    private DateTime geburtstag = DateTime.Now.Date;

    partial void OnGeburtstagChanged(DateTime value)
    {
        ValidateProperty(value, nameof(Geburtstag));
    }

    [ObservableProperty]
    private bool result;

    [RelayCommand(CanExecute=nameof(HasErrors))]
    public void Ok()
    {
        model.Vorname = Vorname;
        model.Nachname = Nachname;
        model.Geburtstag = Geburtstag;
        Result = true;
    }

    [RelayCommand]
    public void Cancel()
    {
        Result = false;
    }

    public PersonEditViewModel(Person model)
    {
        this.model = model;
        this.Vorname = model.Vorname;
        this.Nachname = model.Nachname;
        this.Geburtstag = model.Geburtstag;
    }
}
