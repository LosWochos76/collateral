using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace SeminarManager;

public partial class PersonListViewModel : ObservableObject
{
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
        var viewmodel = new PersonViewModel(navigation, repository, SelectedElement);
        navigation.NavigateTo(viewmodel);
    }

    [RelayCommand]
    public void NewElement()
    {
        var new_person = new Person("", "", DateTime.Now);
        var viewmodel = new PersonViewModel(navigation, repository, new_person);
        navigation.NavigateTo(viewmodel);
    }

    public PersonListViewModel(NavigationStore navigation, DataRepository repository) 
    {
        this.navigation = navigation;
        this.repository = repository;
        UpdateElements();
    }

    private void UpdateElements()
    {
        Elements.Clear();
        foreach (var element in repository.Persons.Elements)
            Elements.Add(element);
    }
}
