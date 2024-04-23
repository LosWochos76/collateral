using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

namespace SeminarManager;

public partial class MainWindow : Window
{
    private NavigationStore navigation;
    public ObservableObject CurrentViewModel { get; set; }

    public MainWindow(NavigationStore navigation)
    {
        InitializeComponent();

        this.navigation = navigation;
        CurrentViewModel = navigation.CurrentViewModel;
        navigation.CurrentViewModelChanged += Navigation_CurrentViewModelChanged;
    }

    private void Navigation_CurrentViewModelChanged()
    {
        CurrentViewModel = navigation.CurrentViewModel;
    }
}
