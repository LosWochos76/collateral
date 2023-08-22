using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var belaege = new List<string>();
        foreach (var control in panel.Children)
        {
            var obj = control as CheckBox;
            if (obj != null && obj.IsChecked.HasValue && obj.IsChecked.Value)
                belaege.Add(obj.Content.ToString());
        }

        var belaege_als_string = string.Join(", ", belaege.ToArray());
        MessageBox.Show("Beläge: " + belaege_als_string);
    }
}