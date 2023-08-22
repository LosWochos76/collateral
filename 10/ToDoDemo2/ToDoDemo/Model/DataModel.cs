using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToDoDemo;

public class DataModel : INotifyPropertyChanged
{
    public ObservableCollection<ToDoItem> Elements { get; set; } = new ObservableCollection<ToDoItem>();
    public event PropertyChangedEventHandler? PropertyChanged;

    private ToDoItem selected_item = null;
    public ToDoItem SelectedItem
    {
        get { return selected_item; }
        set
        {
            selected_item = value;
            IsRemoveButtonEnabled = selected_item != null;
            OnPropertyChanged(nameof(SelectedItem));
            OnPropertyChanged(nameof(IsRemoveButtonEnabled));
        }
    }

    public bool IsRemoveButtonEnabled { get; set; } = false;

    public DataModel()
    {
        Elements.Add(new ToDoItem("Für Praktische Informatik lernen", 50));
        Elements.Add(new ToDoItem("Projekt in Praktische Informatik erstellen", 0));
    }

    public void AddNew()
    {
        Elements.Add(new ToDoItem("Neues Element", 0));
    }

    public void RemoveSelected()
    {
        Elements.Remove(SelectedItem);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
