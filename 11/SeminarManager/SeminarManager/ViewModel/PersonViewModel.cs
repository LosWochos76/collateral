using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace SeminarManager;

public partial class PersonViewModel : ObservableValidator
{
    private NavigationStore navigation;
    private DataRepository repository;

    public Person Model { get; set; }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [MinLength(3)]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string vorname = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(AllowEmptyStrings = false)]
    [MinLength(3)]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string nachname = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private DateTime geburtstag = DateTime.Now.Date;

    public string FullName { get { return Vorname + " " + Nachname; } }

    public bool HasNoErrors { get { return !HasErrors; } }

    [RelayCommand(CanExecute=nameof(HasNoErrors))]
    public void Ok()
    {
        Model.Vorname = Vorname;
        Model.Nachname = Nachname;
        Model.Geburtstag = Geburtstag;

        repository.Persons.Save(Model);

        navigation.NavigateTo(new PersonListViewModel(navigation, repository));
    }

    [RelayCommand]
    public void Cancel()
    {
        navigation.NavigateTo(new PersonListViewModel(navigation, repository));
    }

    public PersonViewModel(NavigationStore navigation, DataRepository repository, Person model)
    {
        this.navigation = navigation;
        this.repository = repository;
        Model = model;

        Vorname = model.Vorname;
        Nachname = model.Nachname;

        ValidateAllProperties();
    }
}
