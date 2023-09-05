using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Specialized;
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
        navigation.CurrentViewModelChanged += Navigation_CurrentViewModelChanged;
    }

    private void Navigation_CurrentViewModelChanged()
    {
        CurrentViewModel = navigation.CurrentViewModel;
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
    }

    [RelayCommand]
    public void NavigateToSeminare()
    {
        navigation.NavigateTo(new SeminarListViewModel(navigation, repository));
        NavigateToPersonenCommand.NotifyCanExecuteChanged();
        NavigateToSeminareCommand.NotifyCanExecuteChanged();
    }
}