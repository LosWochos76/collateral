using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Windows;

namespace SeminarManager;

public partial class MainViewModel : ObservableObject
{
    private IServiceProvider services;
    private ILogger<MainViewModel> logger;
    private NavigationStore navigation;
    private DataRepository repository;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPersonView))]
    [NotifyPropertyChangedFor(nameof(IsSeminarView))]
    private ObservableObject currentViewModel;

    public bool IsPersonView { get { return CurrentViewModel.GetType().Equals(typeof(PersonListViewModel)); } }
    public bool IsSeminarView { get { return CurrentViewModel.GetType().Equals(typeof(SeminarListViewModel)); } }

    public MainViewModel(IServiceProvider services) 
    {
        this.services = services;
        logger = services.GetRequiredService<ILogger<MainViewModel>>();
        navigation = services.GetRequiredService<NavigationStore>();
        repository = services.GetRequiredService<DataRepository>();

        CurrentViewModel = navigation.CurrentViewModel;
        navigation.CurrentViewModelChanged += () => { CurrentViewModel = navigation.CurrentViewModel; };

        logger.LogInformation("MainViewModel created");
    }

    [RelayCommand]
    public void ExitApplication()
    {
        Application.Current.Shutdown();
    }

    [RelayCommand]
    public void NavigateToPersonen()
    {
        var viewModel = services.GetRequiredService<PersonListViewModel>();
        navigation.NavigateTo(viewModel);
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
        JsonSerializer.LoadFromFile(repository, "daten.json");
        NavigateToPersonen();
    }
}