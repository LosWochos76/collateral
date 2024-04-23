using System;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SeminarManager;

class App : Application
{
    public NavigationStore Navigation { get; private set; }
    public DataRepository Repository { get; private set; }

    [STAThread]
    public static void Main(string[] args)
    {
        var app = new App();
        app.Run();
    }

    public App()
    {
        Repository = new DataRepository();
        Navigation = new NavigationStore();
        Navigation.CurrentViewModel = new PersonListViewModel(Navigation, Repository);
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var main = new MainWindow(Navigation) { DataContext = new MainViewModel(Navigation, Repository) };
        main.Show();
    }
}