using System.Collections.ObjectModel;
using System.Windows;

namespace ToDoDemo;

public partial class MainWindow : Window
{
    public ObservableCollection<ToDoItem> Elements { get; set; }

    public MainWindow()
    {
        InitializeComponent();

        Elements = new ObservableCollection<ToDoItem>()
        {
            new ToDoItem("Für Praktische Informatik lernen", 50),
            new ToDoItem("Projekt in Praktische Informatik erstellen", 0)
        };

        DataContext = this;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Elements.Add(new ToDoItem("Vorlesung nochmal nachbereiten", 0));
    }
}
