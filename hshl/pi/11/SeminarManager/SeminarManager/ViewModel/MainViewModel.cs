using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace SeminarManager;

public partial class MainViewModel : ObservableObject
{
    private NavigationStore navigation;
    private DataRepository repository;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPersonView))]
    [NotifyPropertyChangedFor(nameof(IsSeminarView))]
    private ObservableObject currentViewModel;

    public bool IsPersonView { get { return CurrentViewModel.GetType().Equals(typeof(PersonListViewModel)); } }
    public bool IsSeminarView { get { return CurrentViewModel.GetType().Equals(typeof(SeminarListViewModel)); } }

    public MainViewModel(NavigationStore navigation, DataRepository repository) 
    {
        this.navigation = navigation;
        this.repository = repository;

        CurrentViewModel = navigation.CurrentViewModel;
        navigation.CurrentViewModelChanged += () => { CurrentViewModel = navigation.CurrentViewModel; };
    }

    [RelayCommand]
    public void ExitApplication()
    {
        Application.Current.Shutdown();
    }

    [RelayCommand]
    public void NavigateToPersonen()
    {
        navigation.NavigateTo(new PersonListViewModel(navigation, repository));
        NavigateToPersonenCommand.NotifyCanExecuteChanged();
        NavigateToSeminareCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    public void NavigateToSeminare()
    {
        navigation.NavigateTo(new SeminarListViewModel(navigation, repository));
        NavigateToPersonenCommand.NotifyCanExecuteChanged();
        NavigateToSeminareCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    public void Save()
    {
        JsonSerializer.SaveToFile(repository, "daten.json");
    }

    [RelayCommand]
    public void Load()
    {
        var result = JsonSerializer.LoadFromFile("daten.json");
        if (result != null)
            this.repository = result;

        NavigateToPersonen();
    }
}