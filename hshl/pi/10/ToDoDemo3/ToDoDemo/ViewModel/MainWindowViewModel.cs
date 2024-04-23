using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ToDoDemo;

public partial class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel()
    {
        Elements.Add(new ToDoItem("Für Praktische Informatik lernen", 50));
        Elements.Add(new ToDoItem("Projekt in Praktische Informatik erstellen", 0));
    }

    public ObservableCollection<ToDoItem> Elements { get; set; } = new();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RemoveSelectedItemCommand))]
    [NotifyPropertyChangedFor(nameof(IsElementSelected))]
    private ToDoItem selectedItem = null;

    public bool IsElementSelected { get { return SelectedItem != null; } }

    [RelayCommand]
    public void AddNewItem()
    {
        Elements.Add(new ToDoItem("Neues Element", 0));
    }

    [RelayCommand(CanExecute = nameof(IsElementSelected))]
    public void RemoveSelectedItem()
    {
        if (IsElementSelected)
            Elements.Remove(SelectedItem);
    }
}