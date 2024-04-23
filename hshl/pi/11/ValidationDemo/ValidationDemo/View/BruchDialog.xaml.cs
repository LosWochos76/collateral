using System.Windows;

namespace ValidationDemo;

public partial class BruchDialog : Window
{
    private Bruch original;
    private Bruch arbeitskopie;

    public BruchDialog(Bruch bruch)
    {
        InitializeComponent();

        original = bruch;
        arbeitskopie = bruch.Clone();
        DataContext = arbeitskopie;
    }

    private void Button_Ok_Click(object sender, RoutedEventArgs e)
    {
        original.Zaehler = arbeitskopie.Zaehler;
        original.Nenner = arbeitskopie.Nenner;
        DialogResult = true;
        Close();
    }

    private void Button_Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}
