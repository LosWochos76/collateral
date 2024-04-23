using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace SeminarManager;

public partial class PersonViewModel : ObservableValidator
{
    private IServiceProvider services;
    private NavigationStore navigation;
    private DataRepository repository;
    private Person model;

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
        model.Vorname = Vorname;
        model.Nachname = Nachname;
        model.Geburtstag = Geburtstag;

        repository.Persons.Save(model);

        var viewModel = services.GetRequiredService<PersonListViewModel>();
        navigation.NavigateTo(viewModel);
    }

    [RelayCommand]
    public void Cancel()
    {
        var viewModel = services.GetRequiredService<PersonListViewModel>();
        navigation.NavigateTo(viewModel);
    }

    public PersonViewModel(IServiceProvider services, Person model)
    {
        this.services = services;
        navigation = services.GetRequiredService<NavigationStore>();
        repository = services.GetRequiredService<DataRepository>();
        this.model = model;

        Vorname = model.Vorname;
        Nachname = model.Nachname;
        ValidateAllProperties();
    }
}
