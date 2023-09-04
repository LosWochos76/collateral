using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace SeminarManager;

public partial class MainWindowViewModel : ObservableObject
{
    [RelayCommand]
    public void ExitApplication()
    {
        Application.Current.Shutdown();
    }
}
