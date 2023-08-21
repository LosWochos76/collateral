using System.Windows;

namespace UserControlDemo;

public partial class MainWindow : Window
{
    public User CurrentUser { get; set; } = new User() 
    { 
        Vorname = "Peter", 
        Nachname = "Müller", 
        EMail = "peter.mueller@hshl.de" 
    };

    public MainWindow()
    {
        InitializeComponent();

        DataContext = CurrentUser;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        CurrentUser.Vorname = "Michael";
        CurrentUser.Nachname = "Meier";
        CurrentUser.EMail = "michael.meier@hshl.de";
    }
}
