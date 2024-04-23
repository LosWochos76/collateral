using System.Windows;

namespace RoundButtonDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Button wurde geklickt!");
    }
}
