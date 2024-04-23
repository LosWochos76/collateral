using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace SeminarManager;

public partial class SeminarListViewModel : ObservableObject
{
    private NavigationStore navigation;
    private DataRepository repository;
    public ObservableCollection<Seminar> Elements { get; set; } = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsElementSelected))]
    [NotifyCanExecuteChangedFor(nameof(RemoveElementCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditElementCommand))]
    private Seminar selectedElement = null;

    public bool IsElementSelected { get { return SelectedElement != null; } }

    [RelayCommand(CanExecute = nameof(IsElementSelected))]
    public void RemoveElement()
    {
        repository.Seminars.RemoveById(SelectedElement.ID);
        UpdateElements();
    }

    [RelayCommand(CanExecute = nameof(IsElementSelected))]
    public void EditElement()
    {
        var viewmodel = new SeminarViewModel(navigation, repository, SelectedElement);
        navigation.NavigateTo(viewmodel);
    }

    [RelayCommand]
    public void NewElement()
    {
        var obj = new SeminarViewModel(navigation, repository, new Seminar(""));
        navigation.NavigateTo(obj);
    }

    public SeminarListViewModel(NavigationStore navigation, DataRepository repository) 
    {
        this.navigation = navigation;
        this.repository = repository;

        UpdateElements();
    }

    private void UpdateElements()
    {
        Elements.Clear();
        foreach (var element in repository.Seminars.Elements)
            Elements.Add(element);
    }
}
