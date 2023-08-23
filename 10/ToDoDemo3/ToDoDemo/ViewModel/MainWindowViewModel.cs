using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ToDoDemo;

public class MainWindowViewModel : INotifyPropertyChanged
{
    public ObservableCollection<ToDoItem> Elements { get; set; } = new ObservableCollection<ToDoItem>();
    public event PropertyChangedEventHandler? PropertyChanged;

    private NewElementCommandImplementation new_element_command;
    public ICommand NewElementCommand { get { return new_element_command; } }

    private RemoveCurrentElementCommandImplementation remove_current_element_command;
    public ICommand RemoveCurrentElementCommand { get { return remove_current_element_command; } }

    public MainWindowViewModel()
    {
        new_element_command = new NewElementCommandImplementation(this);
        remove_current_element_command = new RemoveCurrentElementCommandImplementation(this);

        Elements.Add(new ToDoItem("Für Praktische Informatik lernen", 50));
        Elements.Add(new ToDoItem("Projekt in Praktische Informatik erstellen", 0));
    }

    private ToDoItem selected_item = null;
    public ToDoItem SelectedItem
    {
        get { return selected_item; }
        set
        {
            selected_item = value;
            OnPropertyChanged(nameof(SelectedItem));
            remove_current_element_command.NotifyCanExecuteChanged();
        }
    }

    public void AddNewItem()
    {
        Elements.Add(new ToDoItem("Neues Element", 0));
    }

    public void RemoveSelectedItem()
    {
        if (SelectedItem != null)
            Elements.Remove(SelectedItem);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void NewElement()
    {
        Elements.Add(new ToDoItem("Neues Element", 0));
    }

    public void RemoveCurrentElement()
    {
        Elements.Remove(SelectedItem);
    }

    internal class NewElementCommandImplementation : ICommand
    {
        private MainWindowViewModel model;
        public event EventHandler? CanExecuteChanged;

        public NewElementCommandImplementation(MainWindowViewModel model)
        {
            this.model = model;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            model.NewElement();
        }
    }

    internal class RemoveCurrentElementCommandImplementation : ICommand
    {
        private MainWindowViewModel model;
        public event EventHandler? CanExecuteChanged;

        public RemoveCurrentElementCommandImplementation(MainWindowViewModel model)
        {
            this.model = model;
        }

        public bool CanExecute(object? parameter)
        {
            return model.SelectedItem != null;
        }

        public void Execute(object? parameter)
        {
            if (CanExecute(null))
                this.model.RemoveCurrentElement();
        }

        public void NotifyCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}