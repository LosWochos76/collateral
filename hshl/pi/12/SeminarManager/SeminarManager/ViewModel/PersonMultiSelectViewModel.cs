using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace SeminarManager;

public partial class PersonMultiSelectViewModel : ObservableObject
{
    public ObservableCollection<Person> UnselectedElements { get; set; }
    public ObservableCollection<Person> SelectedElements { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsItemSelectable))]
    [NotifyCanExecuteChangedFor(nameof(SelectItemCommand))]
    private Person selectableItem;

    public bool IsItemSelectable { get { return SelectableItem != null; } }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsItemRemovable))]
    [NotifyCanExecuteChangedFor(nameof(RemoveItemCommand))]
    private Person removableItem;

    public bool IsItemRemovable { get { return RemovableItem != null; } }

    [RelayCommand(CanExecute=nameof(IsItemSelectable))]
    public void SelectItem()
    {
        SelectedElements.Add(SelectableItem);
        UnselectedElements.Remove(SelectableItem);
    }

    [RelayCommand(CanExecute = nameof(IsItemRemovable))]
    public void RemoveItem()
    {
        UnselectedElements.Add(RemovableItem);
        SelectedElements.Remove(RemovableItem);
    }

    public PersonMultiSelectViewModel(DataRepository repository, ObservableCollection<Person> selectedElements)
    {
        UnselectedElements = new ObservableCollection<Person>();
        SelectedElements = selectedElements;

        foreach (var obj in repository.Persons.Elements) 
        {
           if (!SelectedElements.Contains(obj))
                UnselectedElements.Add(obj);
        }
    }
}