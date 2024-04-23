using System.Windows;

namespace ValidationDemo;

public partial class MainWindow : Window
{
    public Bruch Bruch { get; set; }
    public EmailDialogViewModel Email { get; set; }
    public PersonDialogViewModel Person { get; set; }
    
    public MainWindow()
    {
        InitializeComponent();

        Person = new PersonDialogViewModel("Alexander Stuckenholz", 47, "alexander.stuckenhol@hshl.de");
        Email = new EmailDialogViewModel("alexander.stuckenholz@hshl.de");
        Bruch = new Bruch(1, 3);
    }

    private void Button_ShowBruchDialog_Click(object sender, RoutedEventArgs e)
    {
        var bd = new BruchDialog(Bruch);
        bd.ShowDialog();
    }

    private void Button_ShowEmailDialog_Click(object sender, RoutedEventArgs e)
    {
        var clone = Email.Clone();
        var ed = new EmailDialog(clone);
        var result = ed.ShowDialog();
        if (result == true)
        {
            Email = clone;
        }
    }

    private void Button_ShowPersonDialog_Click(object sender, RoutedEventArgs e)
    {
        var clone = Person.Clone();
        var pd = new PersonDialog(clone);
        var result = pd.ShowDialog();
        if (result == true)
        {
            Person = clone;
        }
    }
}
