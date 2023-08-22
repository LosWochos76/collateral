using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
