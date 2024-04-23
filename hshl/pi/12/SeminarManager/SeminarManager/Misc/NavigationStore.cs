using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel;

namespace SeminarManager;

public class NavigationStore
{
    private ObservableObject currentViewModel;
    public ObservableObject CurrentViewModel 
    { 
        get { return currentViewModel; }
        set
        {
            currentViewModel = value;
            NotifyCurrentViewModelChanged();
        }
    }

    public event Action CurrentViewModelChanged;

    private void NotifyCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }

    public void NavigateTo(ObservableObject newViewModel)
    {
        CurrentViewModel = newViewModel;
    }
}