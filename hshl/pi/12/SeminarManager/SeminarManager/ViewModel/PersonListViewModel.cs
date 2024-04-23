using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;

namespace SeminarManager;

public partial class PersonListViewModel : ObservableObject
{
    private IServiceProvider services;
    private NavigationStore navigation;
    private DataRepository repository;
    public ObservableCollection<Person> Elements { get; set; } = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsElementSelected))]
    [NotifyCanExecuteChangedFor(nameof(RemoveElementCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditElementCommand))]
    private Person selectedElement = null;

    public bool IsElementSelected { get { return SelectedElement != null; } }

    [RelayCommand(CanExecute = nameof(IsElementSelected))]
    public void RemoveElement()
    {
        repository.Persons.RemoveById(SelectedElement.ID);
        UpdateElements();
    }

    [RelayCommand(CanExecute = nameof(IsElementSelected))]
    public void EditElement()
    {
        var viewmodel = new PersonViewModel(services, SelectedElement);
        navigation.NavigateTo(viewmodel);
    }

    [RelayCommand]
    public void NewElement()
    {
        var new_person = new Person("", "", DateTime.Now);
        var viewmodel = ActivatorUtilities.CreateInstance<PersonViewModel>(services, new_person);
        navigation.NavigateTo(viewmodel);
    }

    public PersonListViewModel(IServiceProvider services) 
    {
        this.services = services;
        navigation = services.GetRequiredService<NavigationStore>();
        repository = services.GetRequiredService<DataRepository>();
        UpdateElements();
    }

    private void UpdateElements()
    {
        Elements.Clear();
        foreach (var element in repository.Persons.Elements)
            Elements.Add(element);
    }
}
