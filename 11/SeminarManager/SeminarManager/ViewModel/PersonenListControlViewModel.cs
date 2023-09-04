using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace SeminarManager;

public partial class PersonenListControlViewModel : ObservableObject
{
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
        Elements.Remove(SelectedElement);
    }

    [RelayCommand(CanExecute = nameof(IsElementSelected))]
    public void EditElement()
    {
        var dialog = new EditPersonWindow(SelectedElement);
        dialog.ShowDialog();
    }

    [RelayCommand]
    public void NewElement()
    {
        var obj = new Person();
        var dialog = new EditPersonWindow(obj);
        var result = dialog.ShowDialog();
        if (result.HasValue && result.Value)
            Elements.Add(obj);
    }

    public PersonenListControlViewModel() 
    {
        Elements.Add(new Person() { Vorname = "Michael", Nachname = "Meier", Geburtstag = Convert.ToDateTime("1980-06-12") });
    }
}
