using System.Windows;

namespace Tabs;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Forward_Clicked(object sender, RoutedEventArgs e)
    {
        tabs.SelectedIndex = tabs.SelectedIndex == tabs.Items.Count - 1 ? 0 : tabs.SelectedIndex + 1;
    }

    private void Backward_Clicked(object sender, RoutedEventArgs e)
    {
        tabs.SelectedIndex = tabs.SelectedIndex == 0 ? tabs.Items.Count - 1 : tabs.SelectedIndex - 1;
    }
}
