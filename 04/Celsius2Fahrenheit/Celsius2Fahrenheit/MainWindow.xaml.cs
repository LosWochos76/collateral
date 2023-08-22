using System.Windows;
using System.Windows.Controls;

namespace Celsius2Fahrenheit;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        var textbox = sender as TextBox;
        double celsius;

        if (double.TryParse(textbox.Text, out celsius))
            fahrenheit.Content = string.Format("{0:F2} °F", celsius * 9 / 5 + 32);
        else
            fahrenheit.Content = "Eingabefehler!";
    }
}
