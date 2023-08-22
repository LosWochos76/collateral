using System.Collections.ObjectModel;
using System.Windows;

namespace ToDoDemo;

public partial class MainWindow : Window
{
    private DataModel model = new DataModel();

    public MainWindow()
    {
        InitializeComponent();

        DataContext = model;
    }

    private void Button_Add_Click(object sender, RoutedEventArgs e)
    {
        model.AddNew();
    }

    private void Button_Remove_Click(object sender, RoutedEventArgs e)
    {
        model.RemoveSelected();
    }
}
