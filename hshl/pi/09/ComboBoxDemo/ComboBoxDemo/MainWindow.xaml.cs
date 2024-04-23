using System.Collections.ObjectModel;
using System.Windows;

namespace ComboBoxDemo;

public partial class MainWindow : Window
{
    public ObservableCollection<string> Countries { get; set; } = new ();

    public MainWindow()
    {
        InitializeComponent();

        Countries.Add("Deutschland");
        Countries.Add("Österreich");
        Countries.Add("Türkei");
        Countries.Add("Schweden");
        Countries.Add("USA");

        DataContext = this;
    }

    private void Button_next_Country_Click(object sender, RoutedEventArgs e)
    {
        combobox.SelectedIndex = (combobox.SelectedIndex + 1) % Countries.Count;
    }

    private void Button_Deutschland_Click(object sender, RoutedEventArgs e)
    {
        combobox.SelectedItem = "Deutschland";
    }
}
